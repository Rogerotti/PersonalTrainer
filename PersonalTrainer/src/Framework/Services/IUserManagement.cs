using Framework.Models;
using System;

namespace Framework.Services
{
    public interface IUserManagement
    {
        /// <summary>
        /// Dodaje użytkownika.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        void RegisterUser(String userName, String email, String password, Int32 gender, Decimal height, Decimal weight, Int32 age);

        void Login(String userName, String password);

        UserDto GetCurrentUser();

        Boolean Validation(String userName);

        void Logout();
    }
}
