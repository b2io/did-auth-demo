using CardanoSharp.Wallet.Models.Keys;
using CardanoSharp.Wallet;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Compatibility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DidAuthDemo.Maui.ViewModels;

public partial class ScannerViewModel : ObservableObject
{

    [ObservableProperty]
    string barcodePayload;
}
