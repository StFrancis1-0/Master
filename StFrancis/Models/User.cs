using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.Models
{
    public class User: IdentityUser
    {
        public string Surname { get; set; }
        public string OtherNames { get; set; }
        public string ResidentialAddress { get; set; }
        public string Gender { get; set; }
        public int DateOfBirth { get; set; }
        public string ImagePath { get; set; }
        public string WorshipCenter { get; set; }
        public string Organisation { get; set; }
        public string Society { get; set; }
        
        public string Occupation { get; set; }
        
        public string NatureOfWork { get; set; }
        
        public string StateOfOrigin { get; set; }
        public string BCC { get; set; }
        public string MembershipCardNumber { get; set; }
        
        public string MaritalStatus { get; set; }
        public string Suggestion { get; set; }

    }
}
