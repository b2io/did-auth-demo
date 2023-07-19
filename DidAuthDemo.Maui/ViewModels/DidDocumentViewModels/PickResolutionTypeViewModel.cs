using CardanoSharp.Wallet.Models.Keys;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Core.Common;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Core.Derivers;
using DidAuthDemo.Maui.Models;
using DidAuthDemo.Maui.Views.DidDocumentViews;
using System.Text.Json;

namespace DidAuthDemo.Maui.ViewModels.DidDocumentViewModels;

[QueryProperty("NewDid", "NewDid")]
public partial class PickResolutionTypeViewModel : ObservableObject
{
    private Key _key;

    [ObservableProperty]
    Did newDid;

    [ObservableProperty]
    string name;

    [ObservableProperty]
    string selectedResolutionType;

    [ObservableProperty]
    string didTypeName;

    [ObservableProperty]
    string keyName;

    [ObservableProperty]
    bool showDomain;

    [ObservableProperty]
    string domain;

    [ObservableProperty]
    bool showGithubUsername;

    [ObservableProperty]
    string githubUsername;

    [ObservableProperty]
    string password;

    private KeyDatabase keyDatabase;
    private DidDatabase didDatabase;

    public PickResolutionTypeViewModel()
    {
        keyDatabase = new KeyDatabase();
        didDatabase = new DidDatabase();
    }

    [RelayCommand]
    async void CheckDid()
    {
        _key = await keyDatabase.GetAsync(NewDid.KeyId);
        KeyName = _key.Name;

        DidTypeName = ((DidType)NewDid.DidType).ToString();
    }

    [RelayCommand(CanExecute = nameof(CanCreateDid))]
    async void SubmitDid()
    {
        NewDid.Name = Name;
        NewDid.Domain = Domain;
        NewDid.GithubUsername = GithubUsername;

        //determine DID
        NewDid.Identifier = SelectedResolutionType switch
        {
            "Web" => $"did:web:{NewDid.Domain}",
            "Github" => $"did:github:{NewDid.GithubUsername}",
            _ => throw new NotSupportedException()
        };

        NewDid.ResolutionType = SelectedResolutionType switch
        {
            "Web" => ResolutionType.Web.ToString(),
            "Github" => ResolutionType.Github.ToString(),
            _ => throw new NotSupportedException()
        };

        KeyPair didKeyPair = DeriverFactory
            .GetKeyDeriver((DidType)NewDid.DidType)
            .DeriveKey(_key, NewDid.IndexDerivation, Password);

        NewDid.DidDocument = JsonSerializer.Serialize(await DidUtility.GetDidDocument(NewDid.Identifier, didKeyPair.PublicKey));

        await didDatabase.SaveItemAsync(NewDid);
        await Shell.Current.GoToAsync($"../../../{nameof(DidDocumentDetailView)}", new Dictionary<string, object>() { { "Id", NewDid.Id } });
    }

    bool CanCreateDid() => !string.IsNullOrEmpty(Name)
        && !string.IsNullOrEmpty(SelectedResolutionType)
        && !string.IsNullOrEmpty(Password)
        && (!ShowDomain 
            || (ShowDomain && !string.IsNullOrEmpty(Domain))
        )
        && (!ShowGithubUsername
            || (ShowGithubUsername && !string.IsNullOrEmpty(GithubUsername))
        );

    partial void OnNameChanged(string oldValue, string newValue)
    {
        SubmitDidCommand?.NotifyCanExecuteChanged();
    }

    partial void OnSelectedResolutionTypeChanged(string oldValue, string newValue)
    {
        SubmitDidCommand?.NotifyCanExecuteChanged();
        ShowDomain = newValue.Equals("Web");
        ShowGithubUsername = newValue.Equals("Github");
    }

    partial void OnPasswordChanged(string oldValue, string newValue)
    {
        SubmitDidCommand?.NotifyCanExecuteChanged();
    }
}
