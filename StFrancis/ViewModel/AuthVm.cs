using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.ViewModel
{
    public class AuthVm
    {
        [Required(ErrorMessage ="Please enter your email or phone number")]
        public string Phone_Email { get; set; }
        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }
    }
}
