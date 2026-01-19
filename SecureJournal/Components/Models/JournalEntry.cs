using System;
using System.Collections.Generic;

namespace SecureJournal.Models
{
    public class JournalEntry
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime EntryDate { get; set; } = DateTime.Now;

        // Foreign keys
        public int UserId { get; set; }
        public User? User { get; set; }

        public int? MoodId { get; set; }
        public Mood? Mood { get; set; }

        // Many-to-many relationship with Tag
        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        // Computed property to return mood name safely
        public string PrimaryMoodName => Mood != null ? Mood.Name : "No Mood";
        public string UserName => User != null ? User.UserName : "Unknown User";

    }
}
