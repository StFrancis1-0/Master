using StFrancis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.Interfaces
{
    public interface IProfileManager
    {
         Task<ProfileDetails> GetUserById(string UserId);
         Task <List<ProfileDetails>> GetUsers();
        Task<bool> UpdateProfileImage(string UserId);

    }
}
