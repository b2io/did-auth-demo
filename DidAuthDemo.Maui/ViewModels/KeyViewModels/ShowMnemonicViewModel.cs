using CardanoSharp.Wallet;
using CardanoSharp.Wallet.Extensions.Models;
using CardanoSharp.Wallet.Models.Derivations;
using CardanoSharp.Wallet.Models.Keys;
using CommunityToolkit.Maui.Core.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Maui.Data;
using System.Collections.ObjectModel;
using DidAuthDemo.Maui.Models;
using System.Text.Json;

namespace DidAuthDemo.Maui.ViewModels.KeyViewModels;

public partial class ShowMnemonicViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<string> words;

    private Mnemonic mnemonic;

    [ObservableProperty]
    string name;

    [ObservableProperty]
    string password;

    [ObservableProperty]
    bool acknowledged;

    private readonly KeyDatabase _keyDatabase;

    public ShowMnemonicViewModel()
    {
        _keyDatabase = new KeyDatabase();
    }

    [RelayCommand]
    void GenerateMnemonic()
    {
        mnemonic = new MnemonicService().Generate(15);
        Words = mnemonic.Words.Split(" ").ToObservableCollection();
    }

    [RelayCommand(CanExecute = nameof(CanCreateKey))]
    async void CreateKey()
    {
        MasterNodeDerivation masterNode = mnemonic.GetMasterNode();
        var key = new Key() 
        { 
            Name = Name,
            PrivateKey = JsonSerializer.Serialize(masterNode.PrivateKey.Encrypt(Password)),
            PublicKey = JsonSerializer.Serialize(masterNode.PublicKey)
        };
        await _keyDatabase.SaveItemAsync(key);
        await Shell.Current.GoToAsync("..");
    }

    bool CanCreateKey() => Acknowledged 
        && !string.IsNullOrEmpty(Name) 
        && !string.IsNullOrEmpty(Password);

    partial void OnAcknowledgedChanged(bool oldValue, bool newValue)
    {
        CreateKeyCommand?.NotifyCanExecuteChanged();
    }

    partial void OnNameChanged(string oldValue, string newValue)
    {
        CreateKeyCommand?.NotifyCanExecuteChanged();
    }

    partial void OnPasswordChanged(string oldValue, string newValue)
    {
        CreateKeyCommand?.NotifyCanExecuteChanged();
    }
}
