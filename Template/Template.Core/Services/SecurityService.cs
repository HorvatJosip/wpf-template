using System.Security;
using Template.Core.DAL;

namespace Template.Core
{
    public class SecurityService : ISecurityService<Login>
    {
        public bool Login(string username, SecureString password)
        {
            return username == "admin" && Utils.Unsecure(password) == "admin";
        }

        public Login Register(string username, SecureString password)
        {
            return new Login
            {
                Username = username,
            };
        }
    }
}
