using Microsoft.Extensions.Logging;

using Mopups.Hosting;



using CommunityToolkit.Maui;

using Mopups.Hosting;



namespace ConectaProApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureMopups(); // Mopups corretamente encadeado

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
