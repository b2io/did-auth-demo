using DidAuthDemo.Maui.ViewModels;
using ZXing.Net.Maui;

namespace DidAuthDemo.Maui.Views;

public partial class ScannerView : ContentPage
{
    public ScannerView()
    {
        InitializeComponent();
        BindingContext = new ScannerViewModel();
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = true,
        };
        cameraBarcodeReaderView.WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density;
        cameraBarcodeReaderView.HeightRequest = DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density;

        Unloaded += (sender, e) => { 
            cameraBarcodeReaderView.Handler.DisconnectHandler(); 
        };
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            ((ScannerViewModel)BindingContext).BarcodePayload = e.Results[0].Value;
        });
    }
}