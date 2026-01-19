using SecureJournal.Data;
using SecureJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SecureJournal.Services
{
    public class AnalyticsService
    {
        private readonly JournalService _journalService;

        public AnalyticsService(JournalService journalService)
        {
            _journalService = journalService;
        }

        // Example: Word trends
        public List<(DateTime Date, int AvgWords)> WordTrend()
        {
            var entries = _journalService.GetEntries();
            var grouped = entries
                .GroupBy(e => e.EntryDate.Date)
                .Select(g => (Date: g.Key, AvgWords: (int)g.Average(e => e.Content.Split(' ').Length)))
                .OrderBy(x => x.Date)
                .ToList();

            return grouped;
        }

        // Example: Most used tags
        public Dictionary<string, int> MostUsedTags()
        {
            var entries = _journalService.GetEntries();
            return entries
                .SelectMany(e => e.Tags)
                .GroupBy(t => t.Name)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}