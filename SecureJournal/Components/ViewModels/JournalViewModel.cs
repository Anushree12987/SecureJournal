using SecureJournal.Models;
using SecureJournal.Services;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace SecureJournal.ViewModels
{
    public class JournalViewModel
    {
        private readonly JournalService _journalService;

        public JournalViewModel()
        {
            // Build the path to the SQLite database
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "journal.db");

            // Pass the dbPath to JournalService
            _journalService = new JournalService(dbPath);

            // Load entries initially
            LoadEntries();
        }

        // List of all entries (Observable for UI binding)
        public ObservableCollection<JournalEntry> Entries { get; set; } = new();

        // The entry currently being edited
        public JournalEntry EditingEntry { get; set; } = new JournalEntry { EntryDate = DateTime.Today };

        // Search text
        public string SearchText { get; set; } = "";

        // Load entries from DB and apply search filter
        public void LoadEntries()
        {
            var all = _journalService.GetEntries();
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? all
                : all.Where(e => e.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

            Entries.Clear();
            foreach (var entry in filtered)
                Entries.Add(entry);
        }

        // Save or update current editing entry
        public void SaveEntry()
        {
            _journalService.CreateOrUpdateEntry(EditingEntry);
            EditingEntry = new JournalEntry { EntryDate = DateTime.Today };
            LoadEntries();
        }

        // Delete an entry
        public void DeleteEntry(JournalEntry entry)
        {
            _journalService.DeleteEntry(entry.Id);
            LoadEntries();
        }

        // Load an entry for editing
        public void EditEntry(JournalEntry entry)
        {
            EditingEntry = entry;
        }
    }
}
