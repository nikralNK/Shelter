using System;
using BCrypt.Net;
using ShelterApp.Models;
using ShelterApp.Repository;

namespace ShelterApp.Services
{
    public class AuthService
    {
        private readonly UserRepository userRepository;

        public AuthService()
        {
            userRepository = new UserRepository();
        }

        public User Login(string username, string password)
        {
            var user = userRepository.GetByUsername(username);
            if (user != null && VerifyPassword(password, user.PasswordHash))
            {
                SessionManager.CurrentUser = user;
                return user;
            }
            return null;
        }

        public bool Register(string username, string password, string email, string fullName)
        {
            if (userRepository.UsernameExists(username))
            {
                return false;
            }

            if (userRepository.EmailExists(email))
            {
                return false;
            }

            var user = new User
            {
                Username = username,
                PasswordHash = HashPassword(password),
                Email = email,
                FullName = fullName,
                Role = "User"
            };

            userRepository.Add(user);
            return true;
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch
            {
                return false;
            }
        }

        public void Logout()
        {
            SessionManager.CurrentUser = null;
        }

        public void ResetPassword(string username, string newPassword)
        {
            var passwordHash = HashPassword(newPassword);
            userRepository.UpdatePassword(username, passwordHash);
        }
    }
}
