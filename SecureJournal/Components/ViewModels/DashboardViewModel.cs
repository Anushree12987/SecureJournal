using System.Collections.Generic;
using SecureJournal.Models;

namespace SecureJournal.ViewModels;

public class DashboardViewModel
{
    // Mood name -> count
    public Dictionary<string, int> MoodDistribution { get; } = new();

    // Tag name -> count
    public Dictionary<string, int> TagUsage { get; } = new();

    // Recent journal entries
    public List<JournalEntry> RecentEntries { get; } = new();

    // Calculate analytics from journal entries
    public void CalculateAnalytics(List<JournalEntry> entries)
    {
        MoodDistribution.Clear();
        TagUsage.Clear();
        RecentEntries.Clear();

        if (entries == null || entries.Count == 0)
            return;

        foreach (var entry in entries)
        {
            // ---- Mood count ----
            var moodName = entry.Mood?.Name ?? "No Mood";

            if (MoodDistribution.ContainsKey(moodName))
                MoodDistribution[moodName]++;
            else
                MoodDistribution[moodName] = 1;

            // ---- Tag count ----
            if (entry.Tags == null)
                continue;

            foreach (var tag in entry.Tags)
            {
                if (TagUsage.ContainsKey(tag.Name))
                    TagUsage[tag.Name]++;
                else
                    TagUsage[tag.Name] = 1;
            }
        }

        // Optional: keep last 5 entries
        RecentEntries.AddRange(entries.TakeLast(5));
    }
}