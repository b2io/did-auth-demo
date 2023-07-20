using CommunityToolkit.Maui;
using DidAuthDemo.Maui.Clients;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.ViewModels.DidDocumentViewModels;
using DidAuthDemo.Maui.Views.DidDocumentViews;
using Refit;
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

            builder.Services.AddRefitClient<IAuthClient>()
                .ConfigureHttpClient(x => x.BaseAddress = new Uri("http://10.0.2.2:5196/"));

            builder.Services.AddTransient<DidDocumentAuthRequestScanView>();
            builder.Services.AddTransient<DidDocumentAuthRequestScanViewModel>();

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