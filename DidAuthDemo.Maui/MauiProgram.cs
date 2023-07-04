using CommunityToolkit.Maui;
using DidAuthDemo.Maui.Data;
using ZXing.Net.Maui.Controls;

namespace DidAuthDemo.Maui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder.Services.AddSingleton<KeyDatabase>();
            builder.Services.AddSingleton<DidDatabase>();
            builder.Services.AddSingleton<CredentialDatabase>();

            builder
                .UseMauiApp<App>()
                .UseBarcodeReader()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            return builder.Build();
        }
    }
}