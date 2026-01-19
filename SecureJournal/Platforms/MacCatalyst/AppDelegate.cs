using Foundation;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace SecureJournal;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
    protected override Microsoft.Maui.Hosting.MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}