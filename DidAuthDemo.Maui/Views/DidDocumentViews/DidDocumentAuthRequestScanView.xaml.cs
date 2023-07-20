using DidAuthDemo.Maui.ViewModels.DidDocumentViewModels;
using ZXing.Net.Maui;

namespace DidAuthDemo.Maui.Views.DidDocumentViews;

public partial class DidDocumentAuthRequestScanView : ContentPage
{
	public DidDocumentAuthRequestScanView(DidDocumentAuthRequestScanViewModel viewmodel)
	{
		InitializeComponent();
        BindingContext = viewmodel;
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
            ((DidDocumentAuthRequestScanViewModel)BindingContext).BarcodePayload = e.Results[0].Value;
        });
    }
}