using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Maui.Common;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Models;
using DidAuthDemo.Maui.Views.DidDocumentViews;

namespace DidAuthDemo.Maui.ViewModels.DidDocumentViewModels;

[QueryProperty(nameof(Did), "Did")]
public partial class PickResolutionTypeViewModel : ObservableObject
{
    [ObservableProperty]
    Did did;

    [ObservableProperty]
    string name;

    [ObservableProperty]
    string resolutionType;

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
        var key = await keyDatabase.GetAsync(Did.KeyId);
        KeyName = key.Name;

        DidTypeName = ((DidType)Did.DidType).ToString();
    }

    [RelayCommand(CanExecute = nameof(CanCreateDid))]
    async void SubmitDid()
    {
        Did.Name = Name;
        Did.Domain = Domain; 
        Did.GithubUsername = GithubUsername;

        //determine DID
        Did.Identifier = ResolutionType switch
        {
            "Web" => $"did:web:{Did.Domain}",
            "Github" => $"did:github:{Did.GithubUsername}",
            _ => throw new NotSupportedException()
        };

        await didDatabase.SaveItemAsync(Did);
        await Shell.Current.GoToAsync($"../../../{nameof(DidDocumentDetailView)}", new Dictionary<string, object>() { { "Id", Did.Id } });
    }

    bool CanCreateDid() => !string.IsNullOrEmpty(Name)
        && !string.IsNullOrEmpty(ResolutionType)
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

    partial void OnResolutionTypeChanged(string oldValue, string newValue)
    {
        SubmitDidCommand?.NotifyCanExecuteChanged();
        ShowDomain = newValue.Equals("Web");
        ShowGithubUsername = newValue.Equals("Github");
    }
}
