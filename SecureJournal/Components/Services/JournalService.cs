using System;
using System.Collections.Generic;
using System.Linq;
using SecureJournal.Data;
using SecureJournal.Models;

namespace SecureJournal.Services
{
    public class JournalService
    {
        private readonly JournalDbContext _context;

        // Constructor requires the path to the SQLite database
        public JournalService(string dbPath)
        {
            _context = new JournalDbContext(dbPath);
        }

        // Get all journal entries
        public List<JournalEntry> GetEntries()
        {
            return _context.GetEntries();
        }

        // Get a specific entry by Id
        public JournalEntry? GetEntryById(int id)
        {
            return _context.GetEntries().FirstOrDefault(e => e.Id == id);
        }

        // Create a new entry or update an existing one
        public void CreateOrUpdateEntry(JournalEntry entry)
        {
            _context.CreateOrUpdateEntry(entry);
        }

        // Delete an entry by Id
        public void DeleteEntry(int entryId)
        {
            var entry = _context.GetEntries().FirstOrDefault(e => e.Id == entryId);
            if (entry != null)
                _context.DeleteEntry(entry.EntryDate);
        }

        // Get entries filtered by user (optional)
        public List<JournalEntry> GetEntriesByUser(int userId)
        {
            return _context.GetEntries().Where(e => e.UserId == userId).ToList();
        }
        public List<Mood> GetMoods()
        {
            return _context.GetMoods(); // assuming JournalDbContext has GetMoods()
        }
        public void SaveEntry(JournalEntry entry)
        {
            CreateOrUpdateEntry(entry);
        }



        // Get recent entries (e.g., last n entries)
        public List<JournalEntry> GetRecentEntries(int count)
        {
            return _context.GetEntries()
                .OrderByDescending(e => e.EntryDate)
                .Take(count)
                .ToList();
        }
    }
}