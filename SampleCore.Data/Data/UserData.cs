using Microsoft.EntityFrameworkCore;
using SampleCore.Entities;
using SampleCore.Utilities;
using SampleCoreApi;
using System.Linq;
using System.Threading.Tasks;
using static SampleCore.Utilities.Constants;

namespace SampleCore.Data
{
    public class UserData: IUserData
    {
        private readonly SampleCoreDbContext _context;

        public UserData(SampleCoreDbContext context)
        {
            _context = context;
        }
        public async Task<string> UserSignUp(UserInfo userInfo)
        {
            UserInfo user = await _context.UserInfos.Where(x => x.Email == userInfo.Email).SingleOrDefaultAsync();
            user.Password = Hashing.HashPassword(user.Password);
            if (user != null)
            {
                return ValidationMessages.EMAIL_ALREADY_EXIST;
            }
            else
            {
                _context.UserInfos.Add(userInfo);
                await _context.SaveChangesAsync();

                return ValidationMessages.USER_CREATED;
            }
        }

        public async Task<string> GetUser(string email, string password)
        {
            UserInfo user = await _context.UserInfos.SingleOrDefaultAsync(x => x.Email == email);
            if(user != null)
            {
                var isVerified = Hashing.ValidatePassword(password, user.Password);
                if (isVerified)
                {
                    return ValidationMessages.VALID_USER;
                }
                else
                {
                    return ValidationMessages.INVALID_LOGIN;
                }
            }
            else
            {
                return ValidationMessages.INVALID_LOGIN;
            }
        }
    }
}
