using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StFrancis.Interfaces;
using StFrancis.Models;

namespace StFrancis.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IActivityManager _manager;

        public AdminController(IActivityManager manager)
        {
            _manager = manager;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }


        // GET: Admin/Create
        [HttpGet]
        [Route("[action]")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Create
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult> Create([FromForm]Event model)
        {
            try
            {
                var request = await  _manager.CreateActivity(model);

                if (request.Item1)
                {
                    return Json(new { status = request.Item1, data = "Event successfully created" });
                }
                return  Json(new { status = request.Item1, data = request.Item2 });
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        [Route("[action]")]
        public async Task<ActionResult> GetEvents()
        {
            try
            {
                var request = await _manager.GetEVents();
                return Json(new { status = true, data = request});
            }
            catch
            {
                return View();
            }

        }


    }
}