using SecureJournal.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SecureJournal.Services
{
    public class ExportService
    {
        // This method will be called from Analytics.razor
        public void ExportToPdf(List<JournalEntry> entries, string fileName)
        {
            // For now, save as text. Later you can replace with real PDF library.
            var content = string.Join("\n", entries.Select(e => $"{e.EntryDate.ToShortDateString()} - {e.Title}: {e.Content}"));
            File.WriteAllText(fileName, content);
        }
    }
}