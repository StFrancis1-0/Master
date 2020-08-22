using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StFrancis.Data;
using StFrancis.Interfaces;
using StFrancis.Models;
using StFrancis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.Services
{
    public class ProfileManager : IProfileManager
    {

        string Surname = "";
        string Firstname = "";

        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signinManager;



        public ProfileManager(AppDbContext context, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signinManager = signInManager; 
        }



        public async Task<ProfileDetails> GetUserByIdAsync(string UserId)
        {
            try
            {
                var user =  _context.Users.Where(x => x.Id == UserId).FirstOrDefault();

                //user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == UserId);

                Firstname = user.OtherNames;
                Surname = user.Surname;

                ProfileDetails details = new ProfileDetails
                {
                    Id = user.Id,
                    Surname = user.Surname,
                    OtherName = user.OtherNames,
                    Society = user.Organisation,
                    ProfileImage = GetImagePath(user.ImagePath),
                    DisplayName = GetDisplayName(Surname, Firstname),
                    Occupation = user.Occupation,
                    Parish = user.WorshipCenter

                };



                //ProfileDetails details = new User();
                return details;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            
        }

        private string GetImagePath(string imgPath)
        {
            var cleanPath = imgPath.Replace(@"\", "/");
            return cleanPath;
        }

        private string GetDisplayName(string name1, string name2)
        {
            var displayName = name1 + " " + name2;
            return displayName;
        }

        public Task<List<ProfileDetails>> GetUsers()
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProfileImage(string UserId)
        {
            throw new NotImplementedException();
        }
    }
}
