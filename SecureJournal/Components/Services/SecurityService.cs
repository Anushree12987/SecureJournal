using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using SecureJournal.Data;
using SecureJournal.Models;

namespace SecureJournal.Services
{
    public class SecurityService
    {
        private readonly JournalDbContext _context;

        public SecurityService(string dbPath)
        {
            _context = new JournalDbContext(dbPath);
        }

        // --------------------
        // Register new user
        // --------------------
        public bool Register(string username, string password)
        {
            var users = _context.GetUsers();

            if (users.Any(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase)))
                return false;

            users.Add(new User
            {
                UserName = username,
                PasswordHash = HashPassword(password)
            });

            _context.SaveUsers(users); // <-- REQUIRED helper method
            return true;
        }

        // --------------------
        // Login
        // --------------------
        public bool Login(string username, string password)
        {
            var user = _context.GetUsers()
                .FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                return false;

            return user.PasswordHash == HashPassword(password);
        }

        // --------------------
        // Change password
        // --------------------
        public bool ChangePassword(string username, string newPassword)
        {
            var users = _context.GetUsers();

            var user = users
                .FirstOrDefault(u => u.UserName.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
                return false;

            user.PasswordHash = HashPassword(newPassword);
            _context.SaveUsers(users); // persist change

            return true;
        }

        // --------------------
        // Hashing
        // --------------------
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
