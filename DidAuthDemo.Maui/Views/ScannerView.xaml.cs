using ZXing.Net.Maui;

namespace DidAuthDemo.Maui.Views;

public partial class ScannerView : ContentPage
{
    public ScannerView()
    {
        InitializeComponent();
        cameraBarcodeReaderView.Options = new BarcodeReaderOptions
        {
            Formats = BarcodeFormat.QrCode,
            AutoRotate = true,
            Multiple = true,
        };
        cameraBarcodeReaderView.WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / DeviceDisplay.Current.MainDisplayInfo.Density;
        cameraBarcodeReaderView.HeightRequest = DeviceDisplay.Current.MainDisplayInfo.Height / DeviceDisplay.Current.MainDisplayInfo.Density;
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        Dispatcher.Dispatch(() =>
        {
            barcodeReader.Text = $"{e.Results[0].Value} {e.Results[0].Format}";
        });
    }
}