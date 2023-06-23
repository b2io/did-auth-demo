using System.Diagnostics;
using ZXing.Net.Maui;

namespace DidAuthDemo.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            cameraBarcodeReaderView.Options = new BarcodeReaderOptions
            {
                Formats = BarcodeFormat.QrCode,
                AutoRotate = true,
                Multiple = true
            };
        }

        protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
        {
            Dispatcher.Dispatch(() =>
            {
                barcodeReader.Text = $"{e.Results[0].Value} {e.Results[0].Format}";
            });
        }
    }
}