using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.ViewModel
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImagePath { get; set; }
        public string PhoneNumber { get; set; }
    }
}
