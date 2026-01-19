using System;
using System.Collections.Generic;

namespace SecureJournal.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property: a user can have many journal entries
        public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }
}