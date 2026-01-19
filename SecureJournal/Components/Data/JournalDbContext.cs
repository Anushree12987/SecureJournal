using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using SecureJournal.Models;

namespace SecureJournal.Data
{
    public class JournalDbContext
    {
        private readonly SQLiteConnection _connection;

        public JournalDbContext(string dbPath)
        {
            _connection = new SQLiteConnection(dbPath);

            // Create tables if not exists
            _connection.CreateTable<JournalEntry>();
            _connection.CreateTable<User>();
            _connection.CreateTable<Mood>();
            _connection.CreateTable<Tag>();
        }

        // ---------------------------
        // JournalEntry methods
        // ---------------------------
        public List<JournalEntry> GetEntries() => _connection.Table<JournalEntry>().ToList();

        public void CreateOrUpdateEntry(JournalEntry entry)
        {
            var existing = _connection.Table<JournalEntry>()
                .FirstOrDefault(e => e.Id == entry.Id);

            if (existing != null)
                _connection.Update(entry);
            else
                _connection.Insert(entry);
        }

        public void DeleteEntry(DateTime date)
        {
            var existing = _connection.Table<JournalEntry>()
                .FirstOrDefault(e => e.EntryDate.Date == date.Date);
            if (existing != null)
                _connection.Delete(existing);
        }
        public void SaveUsers(List<User> users)
        {
            // SQLite / file / in-memory save logic here
        }


        // ---------------------------
        // User methods
        // ---------------------------
        public void InsertUser(User user) => _connection.Insert(user);

        public User? GetUserByUsername(string username) =>
            _connection.Table<User>().FirstOrDefault(u => u.UserName == username);

        public void UpdateUser(User user) => _connection.Update(user);

        public List<User> GetUsers() => _connection.Table<User>().ToList();

        // ---------------------------
        // Mood methods
        // ---------------------------
        public List<Mood> GetMoods() => _connection.Table<Mood>().ToList();

        // ---------------------------
        // Tag methods
        // ---------------------------
        public List<Tag> GetTags() => _connection.Table<Tag>().ToList();
    }
}
