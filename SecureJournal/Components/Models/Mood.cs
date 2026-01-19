using System.Collections.Generic;

namespace SecureJournal.Models
{
    public class Mood
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty; // e.g., Happy, Sad, Neutral
        public string Description { get; set; } = string.Empty;
        
        // Navigation property: multiple entries can have this mood
        public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }
}