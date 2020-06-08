using StFrancis.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StFrancis.Interfaces
{
    public interface IUserManager
    {
        Task<Tuple<bool, string>> Register(RegisterVm registerVm);
        Task<Tuple<bool, string, AuthResponse>> AuthenticateUser(AuthVm login);
        Task<object> RegisterProfession();
    }
}
