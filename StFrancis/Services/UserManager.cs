using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StFrancis.Data;
using StFrancis.Interfaces;
using StFrancis.Models;
using StFrancis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StFrancis.Services
{
    public class UserManager : IUserManager
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;

        public UserManager(AppDbContext context, UserManager<User> userManager, IConfiguration configuration)
        {
           _context = context;
           _userManager = userManager;
           _configuration = configuration;
        }
        public async Task<Tuple<bool, string, AuthResponse>> AuthenticateUser(AuthVm login)
        {
            try
            {
                if (string.IsNullOrEmpty(login.Phone_Email) || string.IsNullOrEmpty(login.Password))
                {
                    return new Tuple<bool, string, AuthResponse>(false, "Fill in all the fields", null);
                }
                User user = new User();
                if (!login.Phone_Email.Contains("@"))
                {
                    user = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == login.Phone_Email);

                }
                else
                {
                    user = await _userManager.FindByEmailAsync(login.Phone_Email);
                }

                var userHasPassword = await _userManager.HasPasswordAsync(user);
                if (!userHasPassword)
                {
                    return new Tuple<bool, string, AuthResponse>(false, "sorry you do not have a valid password", null);
                }

                var userHasValidPassword = await _userManager.CheckPasswordAsync(user, login.Password);
                if (!userHasValidPassword)
                {
                    return new Tuple<bool, string, AuthResponse>(false, "Your password is invalid", null);
                }

                if (user == null)
                {
                    return new Tuple<bool, string, AuthResponse>(false, "Your email/phone number and or password is incorrect", null);
                }


                var claims = new List<Claim>()
                {
                     new Claim(ClaimTypes.NameIdentifier, user.Id),
                     new Claim("FirstName", user.FirstName),
                     new Claim("LastName", user.LastName),
                     new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                };

                var token = JwtTokenGenerator.GenerateAccessToken(claims, _configuration).ToString();
                AuthResponse response = new AuthResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = token,
                    PhoneNumber = user.PhoneNumber,
                    ImagePath = user.ImagePath
                };

                return new Tuple<bool, string, AuthResponse>(true, " ", response);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Tuple<bool, string>> Register(RegisterVm registerVm)
        {
            try
            {
                var checkIfUserExist = await UserExists(registerVm);
                if (checkIfUserExist)
                {
                    return new Tuple<bool, string>(false, "Phone number or email has been taken");
                }


                User userToRegister = new User
                {
                    FirstName = registerVm.FirstName,
                    LastName = registerVm.LastName,
                    Email = registerVm.Email,
                    PhoneNumber = registerVm.PhoneNumber,
                    Sex = registerVm.Sex,
                    DateOfBirth = registerVm.DateOfBirth,
                    UserName = registerVm.Email,
                    Address = registerVm.Address,
                    ImagePath = registerVm.ImagePath
                };

                var result = await _userManager.CreateAsync(userToRegister, registerVm.Password);
                if (!result.Succeeded)
                {
                    return new Tuple<bool, string>(false, result.Errors.FirstOrDefault().Description);
                }


                return new Tuple<bool, string>(true, userToRegister.FirstName + " " + userToRegister.LastName);
            }
            catch (Exception ex)
            {

                return new Tuple<bool, string>(false, "Oops! An error occured");
            }

        }

        public Task<object> RegisterProfession()
        {
            throw new NotImplementedException();
        }

        private async Task<bool> UserExists(RegisterVm registerModel)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(p => p.Email == registerModel.Email || p.PhoneNumber == registerModel.PhoneNumber);
            if(result != null)
            {
                return true;
            }
            return false;
        }
    }
}
