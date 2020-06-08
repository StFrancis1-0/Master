using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.ViewModel
{
    public class RegisterVm
    {
        [Required(ErrorMessage ="Please enter your password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please confirm your password")]
        [Compare("Password", ErrorMessage ="ConfirmPassword does not match password")]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage ="Please enter your first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter your addresss")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your sex")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Please enter your date of birth")]
        public int DateOfBirth { get; set; }
        public string ImagePath { get; set; }

    }
}
