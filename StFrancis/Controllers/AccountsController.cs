using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using StFrancis.Interfaces;
using StFrancis.ViewModel;

namespace StFrancis.Controllers
{
    [Route("[controller]")]
    public class AccountsController : Controller
    {
        private readonly IUserManager _userManager;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AccountsController(IUserManager userManager, IHostingEnvironment hostingEnvironment)
        {
           _userManager = userManager;
           _hostingEnvironment = hostingEnvironment;
        }
        
        [HttpGet]
        [Route("[action]")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Register([FromForm]RegisterVm registerVm)
        {
            var files = Request.Form.Files;
            if (!files.Any())
            {
                return Json(new { status = false, data = "Please upload your profile photo" });
            }
            var file = files[0];
            if(file.Length < 1)
            {
                return Json(new { staus = false, data = "Please upload your profile photo" });
            }
            var webRootPath = _hostingEnvironment.WebRootPath;
            var folderName = "img";
            var pathToSave = Path.Combine(webRootPath, folderName, "ProfilePictures");
            if (!Directory.Exists(pathToSave))
            {
                Directory.CreateDirectory(pathToSave);
            }

            var ext = Path.GetExtension(file.FileName);
            if (string.IsNullOrEmpty(ext))
            {
                return Json(new { status = false, data = "The uploaded file has no extention" });
            }

            var filename = DateTime.Now.Ticks.ToString() + ext;
            var fullpath = Path.Combine(pathToSave, filename);
            var dbPath = Path.Combine("ProfilePicture", filename);
            registerVm.ImagePath = dbPath;
            if (!(filename.ToLower().EndsWith(".jpg") || filename.ToLower().EndsWith(".jpeg") || filename.ToLower().EndsWith(".png")))
            {
                return Json(new { status = false, data = "Unsupported file extention" });
            }


            using (var stream = new FileStream(fullpath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var result = await _userManager.Register(registerVm);
            if(result.Item1)
            {
                return Json(new { status = result.Item1,  data = $"Hello {result.Item2} your registration was successfull. Welcome on board; we are glad to have you." });
            }
            else
            {
                System.IO.File.Delete(fullpath);
                return Json(new { status = result.Item1, data = result.Item2 });
            }
        }

        [Route("[action]")]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

     
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Login([FromBody]AuthVm loginVm)
        {
            var response = await _userManager.AuthenticateUser(loginVm);
            if (response.Item1)
            {
                return Json(new { status = response.Item1, data = response.Item3});
                //return RedirectToAction("register", "accounts");
            }
            return View();
        }


    }
}