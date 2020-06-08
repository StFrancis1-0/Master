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
        public string Surname { get; set; }
        [Required(ErrorMessage = "Please enter your other names")]
        public string OtherNames { get; set; }
        [Required(ErrorMessage = "Please enter your phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please enter your addresss")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your sex")]
        public string Sex { get; set; }
        [Required(ErrorMessage = "Please enter your date of birth")]
        public int DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        [Required(ErrorMessage = "Please select your worship center")]
        public string WorshipCenter { get; set; }
        public string Organisation { get; set; }
        public string Society { get; set; }
        [Required(ErrorMessage = "Please select your occupation")]
        public string Occupation { get; set; }
        [Required(ErrorMessage = "Please enter your nature of work/business")]
        public string NatureOfWork { get; set; }
        [Required(ErrorMessage = "Please enter your state")]
        public string State { get; set; }
        public string BCC { get; set; }
        public string MCardNumber { get; set; }
        [Required(ErrorMessage = "Please select your marital status")]
        public string MaritalStatus { get; set; }
        public string Suggestion { get; set; }

    }
}
