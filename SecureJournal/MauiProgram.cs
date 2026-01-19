using System;
using Microsoft.Extensions.Logging;
using SecureJournal.Services;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;

namespace SecureJournal;

public static class MauiProgram
{
    public static Microsoft.Maui.Hosting.MauiApp CreateMauiApp()
    {
        var builder = Microsoft.Maui.Hosting.MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        string dbPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "journal.db");

        builder.Services.AddSingleton(_ => new JournalService(dbPath));
        builder.Services.AddSingleton(_ => new SecurityService(dbPath));
        builder.Services.AddSingleton<AnalyticsService>();
        builder.Services.AddSingleton<ExportService>();

        return builder.Build();
    }
}
