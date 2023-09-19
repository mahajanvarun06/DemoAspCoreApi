using SampleCore.Entities;
using System.Threading.Tasks;

namespace SampleCore.Data
{
    public interface IUserData
    {
        Task<string> UserSignUp(UserInfo userInfo);

        Task<string> GetUser(string email, string password);
    }
}
