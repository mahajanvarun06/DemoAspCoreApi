using SampleCore.Data;
using SampleCore.Entities;
using System.Threading.Tasks;

namespace SampleCore.Services
{
    public class UserService:  IUserService
    {

        private readonly IUserData _userData;

        public UserService(IUserData userData)
        {
            _userData = userData;
        }

        public async Task<string> UserSignUp(UserInfo userInfo)
        {
            return await _userData.UserSignUp(userInfo);
        }

        public async Task<string> GetUser(string email, string password)
        {
            return await _userData.GetUser(email, password);
        }
    }
}
