using System.Collections.Generic;

namespace SecureJournal.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Navigation property: many-to-many with JournalEntry
        public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }
}
