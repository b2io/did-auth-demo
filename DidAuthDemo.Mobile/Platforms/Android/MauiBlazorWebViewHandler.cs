using Microsoft.AspNetCore.Components.WebView.Maui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidAuthDemo.Mobile.Platforms.Android
{
    // https://stackoverflow.com/questions/70229906/blazor-maui-camera-and-microphone-android-permissions
    public class MauiBlazorWebViewHandler : BlazorWebViewHandler
    {

        protected override global::Android.Webkit.WebView CreatePlatformView()
        {
            var view = base.CreatePlatformView();
            view.SetWebChromeClient(new MyWebChromeClient(this.Context));
            return view;
        }
    }
}
