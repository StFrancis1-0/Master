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
using MimeKit;
using MailKit.Net.Smtp;

namespace StFrancis.Services
{
    public class UserManager : IUserManager
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public string email = "";
        public string fullName = ""; 


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
                     new Claim("Surname", user.Surname),
                     new Claim("OtherNames", user.OtherNames),
                     new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                };

                var token = JwtTokenGenerator.GenerateAccessToken(claims, _configuration).ToString();
                AuthResponse response = new AuthResponse
                {
                    Surname = user.Surname,
                    OtherNames = user.OtherNames,
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

                email = registerVm.Email;
                fullName = registerVm.Surname + " " + registerVm.OtherNames;
                User userToRegister = new User
                {
                    Surname = registerVm.Surname,
                    OtherNames = registerVm.OtherNames,
                    Email = registerVm.Email,
                    PhoneNumber = registerVm.PhoneNumber,
                    Gender = registerVm.Sex,
                    DateOfBirth = registerVm.DateOfBirth,
                    UserName = registerVm.Email,
                    ResidentialAddress = registerVm.Address,
                    ImagePath = registerVm.ImagePath,
                    Organisation = registerVm.Organisation,
                    Society = registerVm.Society,
                    BCC = registerVm.BCC,
                    StateOfOrigin = registerVm.State,
                    NatureOfWork = registerVm.NatureOfWork,
                    Occupation = registerVm.Occupation,
                    WorshipCenter = registerVm.WorshipCenter,
                    Suggestion = registerVm.Suggestion,
                    MaritalStatus = registerVm.MaritalStatus,
                    MembershipCardNumber = registerVm.MCardNumber,
                    
                };
                
                var result = await _userManager.CreateAsync(userToRegister, registerVm.Password);
                if (!result.Succeeded)
                {
                    return new Tuple<bool, string>(false, result.Errors.FirstOrDefault().Description);
                }

                SendMail();

                return new Tuple<bool, string>(true, userToRegister.Surname + " " + userToRegister.OtherNames);
            }
            catch (Exception ex)
            {

                return new Tuple<bool, string>(false, "Oops! An error occured");
            }

        }

        private bool SendMail()
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("StFrancis", "admin@stfrancisoregun.org"));
            message.To.Add(new MailboxAddress("User", email));
            message.Subject = "Welcome to StFrancis Catholic Church";
            message.Body = new TextPart("plain")
            {
                Text = $"Hello, {fullName}\n\n" +
              @"You are welcome to StFrancis Catholic Church.
For further enquiry, kindly send a mail to admin@stfrancisoregun.org

We are happy to have you here.

Thank you."
            };

            using (var client = new SmtpClient())
            {
                //client.Connect("mail.stfrancisoregun.org", 25, false);
                client.Connect("smtp.gmail.com", 587, false);
                //client.Authenticate("admin@stfrancisoregun.org", "Password@1");
                client.Authenticate("scriptchore@gmail.com", "www.osoftit.com@2020");
                client.Send(message);
                client.Disconnect(true);
            }

            return true;
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
