using StFrancis.Interfaces;
using StFrancis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.Services
{
    public class ProfileManager : IProfileManager
    {
        public Task<ProfileDetails> GetUserById(string UserId)
        {
            throw new NotImplementedException();
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
