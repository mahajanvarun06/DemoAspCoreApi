using SampleCore.Entities;
using System.Threading.Tasks;

namespace SampleCore.Services
{
    public interface IUserService
    {
        Task<string> UserSignUp(UserInfo userInfo);

        Task<string> GetUser(string email, string password);
    }
}
