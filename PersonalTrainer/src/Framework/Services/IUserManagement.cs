using Framework.Models;
using System;

namespace Framework.Services
{
    /// <summary>
    /// Manager zarządzająćy użytkownikami.
    /// </summary>
    public interface IUserManagement
    {
        /// <summary>
        /// Rejestruje użytkownika.
        ///  W przypadku błędnej walidacji danych rzuca wyjątkiem  <see cref="UnauthorizedAccessException"/>
        /// </summary>
        /// <param name="userName">Nazwa użytkownika.</param>
        /// <param name="email">E-mail użytkownika.</param>
        /// <param name="password">Hasło użytkownika.</param>
        /// <param name="gender">Płęć użytkownika</param>
        /// <param name="height">Wzrost użytkownika.</param>
        /// <param name="weight">Waga użytkownika.</param>
        /// <param name="age">Wiek użytkownika.</param>
        void RegisterUser(String userName, String email, String password, Int32 gender, Decimal height, Decimal weight, Int32 age);

        /// <summary>
        /// Logowanie użytkownika i zapisanie nazwy oraz id do sesji.
        /// W przypadku błędnej nazwy lub hasła rzuca wyjątkiem <see cref="UnauthorizedAccessException"/>
        /// </summary>
        /// <param name="userName">Nazwa użytkownika.</param>
        /// <param name="password">Hasło użytkownika.</param>
        void Login(String userName, String password);

        /// <summary>
        /// Wylogowywuje użytkownika.
        /// Czyści sesję.
        /// </summary>
        void Logout();

        /// <summary>
        /// Sprawdza czy użytkownik jest zalogowany.
        /// </summary>
        /// <returns></returns>
        Boolean UserLogedIn();

        /// <summary>
        /// Zwraca id aktualnie zalogowanego użytkownika.
        /// W przypadku braku zalogowania rzuca wyjątkiem.<see cref="UnauthorizedAccessException"/>
        /// </summary>
        /// <returns></returns>
        Guid GetCurrentUserId();

        /// <summary>
        /// Zwraca nazwę aktualnie zalogowanego użytkownika.
        /// W przypadku braku zalogowania rzuca wyjątkiem.<see cref="UnauthorizedAccessException"/>
        /// </summary>
        /// <returns></returns>
        String GetCurrentUserName();

        /// <summary>
        /// Zwraca dto aktualnie zalogowanego użytkownika.
        /// W przypadku braku zalogowania rzuca wyjątkiem.<see cref="UnauthorizedAccessException"/>
        /// </summary>
        /// <returns></returns>
        UserDto GetCurrentUser();
    }
}
