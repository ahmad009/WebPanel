using DataAccess;
using DataModel;
using DataModel.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogic
{
    public class UserBO
    {
        DbContextOptions<OshanakCommonContext> _comOptions;
        public static readonly UserBO Instance = new UserBO(new DbContextOptions<OshanakCommonContext>());
        public UserBO(DbContextOptions<OshanakCommonContext> comOptions)
        {
            _comOptions = comOptions;
        }

        public async Task<Web_UserModel?> GetWebUserAsync(String? AccountName)
        {
            if (string.IsNullOrEmpty(AccountName))
                return null;
            using (DataAccess.OshanakCommonContext db = new DataAccess.OshanakCommonContext(_comOptions))
            {
                var query = from p in db.Web_Users
                            where p.AccountName == AccountName
                            select p;
                return await query.FirstOrDefaultAsync();
            }
        }

        public string HashWithSHA256(string value)
        {
            using var hash = SHA256.Create();
            var byteArray = hash.ComputeHash(Encoding.UTF8.GetBytes(value));
            return Convert.ToHexString(byteArray);
        }

        public async Task<Web_UserModel?> AuthenticateUser(string username, string password)
        {
            try
            {
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    var hashedpassword = HashWithSHA256(password).ToUpper();

                    using (DataAccess.OshanakCommonContext db = new DataAccess.OshanakCommonContext(_comOptions))
                    {
                        var query = from p in db.Web_Users
                                    where p.AccountName == username && p.Password == hashedpassword
                                    select p;
                        var user = await query.FirstOrDefaultAsync();

                        if (user != null)
                        {
                            return user;
                        }
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> ChangePassword(UserModel user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.Username) && user.Password.Equals(user.RepeatPassword))
                {
                    var hashedpassword = HashWithSHA256(user.Password).ToUpper();

                    using (DataAccess.OshanakCommonContext db = new DataAccess.OshanakCommonContext(_comOptions))
                    {
                        var query = from p in db.Web_Users
                                    where p.AccountName == user.Username
                                    select p;
                        var webuser = await query.FirstOrDefaultAsync();

                        if (webuser != null)
                        {
                            webuser.Password = hashedpassword;
                            webuser.LastUpdateDate = DateTime.Now;

                            await db.SaveChangesAsync();

                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}