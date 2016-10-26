﻿using Framework.DataBaseContext;
using Framework.Models;
using Framework.Models.Database;
using Framework.Resources;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Framework.Services
{
    public class UserManagement : IUserManagement
    {
        private readonly UserContext context;
        private readonly ISession session;

        private const String userId = nameof(userId);
        private const String userName = nameof(userName);

        public UserManagement(UserContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            session = httpContextAccessor.HttpContext.Session;
        }

        public void RegisterUser(String username, String email, String password, Int32 gender, Decimal height, Decimal weight, Int32 age)
        {
            using (var trans = context.Database.BeginTransaction())
            {
                CheckUsername(username);
                CheckEmail(email);
                CheckAge(age);
                CheckGender(gender);

                var salt = CreateSalt(32);
                byte[] pass = Encoding.UTF8.GetBytes(password);
                var hash = GenerateSaltedHash(pass, salt);

                Guid userId = Guid.NewGuid();

                var user = new User()
                {
                    UserId = userId,
                    UserName = username,
                    HashCode = Convert.ToBase64String(hash),
                    Salt = Convert.ToBase64String(salt),
                    Email = email,
                };

                var userDetails = new UserDetails()
                {
                    UserId = userId,
                    Gender = gender,
                    Height = height,
                    Weight = weight,
                    Age = age,
                    HeightUnit = 0
                };

                userDetails.User = user;
                context.UsersDetails.Add(userDetails);
                context.SaveChanges();
                trans.Commit();
            }
        }

        public void Login(String username, String password)
        {
            var userList = context.Users;
            var user = userList.FirstOrDefault(x => x.UserName.Equals(username));

            if (user == null) throw new UnauthorizedAccessException(ErrorLanguage.UserNameOrPasswordWrong);


            byte[] salt = Convert.FromBase64String(user.Salt);
            byte[] pass = Encoding.UTF8.GetBytes(password);

            var saltedHash = GenerateSaltedHash(pass, salt);

            var hasCode = Convert.FromBase64String(user.HashCode);
            if (hasCode.SequenceEqual(saltedHash))
            {
                session.SetString(userId, user.UserId.ToString());
                session.SetString(userName, user.UserName);
            }
            else
                throw new UnauthorizedAccessException(ErrorLanguage.UserNameOrPasswordWrong);
        }

        public void Logout()
        {
            session.Remove(userId);
            session.Remove(userName);
        }

        public Boolean UserLogedIn()
        {
            var id = session.GetString(userId);
            return String.IsNullOrWhiteSpace(id) ? false : true;
        }

        public Guid GetCurrentUserId()
        {
            var id = session.GetString(userId);

            if (String.IsNullOrWhiteSpace(id)) throw new UnauthorizedAccessException(ErrorLanguage.UserNotLoggedIn);

            return new Guid(id);
        }

        public String GetCurrentUserName()
        {
            var currentUserName = session.GetString(userName);

            if (String.IsNullOrWhiteSpace(currentUserName)) throw new UnauthorizedAccessException(ErrorLanguage.UserNotLoggedIn);

            return currentUserName;
        }

        public UserDto GetCurrentUser()
        {
            Guid userGuid = GetCurrentUserId();

            var userDto = context.Users.FirstOrDefault(x => x.UserId.Equals(userGuid));
            var userDetails = context.UsersDetails.FirstOrDefault(x => x.UserId.Equals(userGuid));

            return new UserDto()
            {
                UserId = userGuid,
                Age = userDetails.Age,
                Height = userDetails.Height,
                HeightUnit = GetHeightUnitType(userDetails.HeightUnit),
                Login = userDto.UserName,
                Email = userDto.Email,
                Password = null,
                PasswordConfirmation = null,
                Weight = userDetails.Weight,
                Gender = userDetails.Gender
            };
        }

        private HeightUnit? GetHeightUnitType(Int32 heightNumber)
        {
            if (heightNumber == 1)
                return HeightUnit.cm;
            else if (heightNumber == 2)
                return HeightUnit.ft;
            else if (heightNumber == 3)
                return HeightUnit.yd;

            return null;
        }

        /// <summary>
        /// Sprawdza nazwę użytkownika.
        /// </summary>
        /// <param name="username">Nazwa użytkownika.</param>
        private void CheckUsername(String username)
        {
            if (String.IsNullOrWhiteSpace(username)) throw new UnauthorizedAccessException(ErrorLanguage.UsernameEmpty);
            if (username.Length <= 2 || username.Length > 20) throw new UnauthorizedAccessException(ErrorLanguage.UsernameLength);

            var users = context.Users;
            if (users.Any(x => x.UserName.Equals(username))) throw new UnauthorizedAccessException(ErrorLanguage.UsernameExist);
        }

        /// <summary>
        /// Sprawdza adres email.
        /// </summary>
        /// <param name="email">adres email.</param>
        private void CheckEmail(String email)
        {
            if (String.IsNullOrWhiteSpace(email)) throw new UnauthorizedAccessException(ErrorLanguage.EmailEmpty);

            String emailPatern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            if (!Regex.Match(email, emailPatern).Success) throw new UnauthorizedAccessException(ErrorLanguage.EmailRegex);
        }

        /// <summary>
        /// Sprawdza płeć
        /// </summary>
        /// <param name="gender">Płeć użytkownika</param>
        private void CheckGender(Int32 gender)
        {
            if (gender != 0 && gender != 1 && gender != 2 && gender != 9)
                throw new UnauthorizedAccessException(ErrorLanguage.GenderError);
        }

        /// <summary>
        /// Sprawdza wiek
        /// od 6 lat do 99 lat.
        /// </summary>
        /// <param name="age">Wiek.</param>
        private void CheckAge(Int32 age)
        {
            if (age >= 100 || age < 6)
                throw new UnauthorizedAccessException(ErrorLanguage.AgeRange);
        }

        private Byte[] CreateSalt(Int32 size)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            Byte[] buff = new Byte[size];

            rng.GetBytes(buff);

            return buff;
        }

        private Byte[] GenerateSaltedHash(Byte[] plainText, Byte[] salt)
        {
            HashAlgorithm algorithm = SHA512.Create();

            Byte[] plainTextWithSaltBytes =
              new Byte[plainText.Length + salt.Length];

            for (Int32 i = 0; i < plainText.Length; i++)
                plainTextWithSaltBytes[i] = plainText[i];

            for (Int32 i = 0; i < salt.Length; i++)
                plainTextWithSaltBytes[plainText.Length + i] = salt[i];

            return algorithm.ComputeHash(plainTextWithSaltBytes);
        }
    }
}
