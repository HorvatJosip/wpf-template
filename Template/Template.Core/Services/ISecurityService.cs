using System.Security;

namespace Template.Core
{
    /// <summary>
    /// Deals with security such as logging in.
    /// </summary>
    public interface ISecurityService<T>
    {
        /// <summary>
        /// Tries to perform a login for the user with given credentials.
        /// Returns true if the login succeeds.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns></returns>
        bool Login(string username, SecureString password);

        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <typeparam name="T">Type that stores the user.</typeparam>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns></returns>
        T Register(string username, SecureString password);
    }
}
