using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DidAuthDemo.Maui.Common;
using DidAuthDemo.Maui.Data;
using DidAuthDemo.Maui.Views.DidDocumentViews;
using DidAuthDemo.Maui.Models;

namespace DidAuthDemo.Maui.ViewModels.DidDocumentViewModels;

[QueryProperty("SelectedKey", "SelectedKey")]
public partial class PickDocumentTypeListViewModel : ObservableObject
{
    [ObservableProperty]
    Key selectedKey;

    [ObservableProperty]
    bool canHaveCardanoStaking;

    private int currentSimpleCount;
    private int currentPaymentCount;

    private readonly DidDatabase _didDatabase;

    public PickDocumentTypeListViewModel()
    {
        _didDatabase = new DidDatabase();
    }

    [RelayCommand]
    async void CheckExistingDidTypes()
    {
        var didsFromKeys = (await _didDatabase.GetByKeyIdAsync(SelectedKey.Id));

        if (!didsFromKeys.Any(x => x.DidType == (int)DidType.CardanoStaking))
            CanHaveCardanoStaking = true;

        currentSimpleCount = didsFromKeys.Count(x => x.DidType == (int)DidType.Simple);
        currentPaymentCount = didsFromKeys.Count(x => x.DidType == (int)DidType.CardanoPayment);
    }

    [RelayCommand]
    async void SelectedDidType(string type)
    {
        DidType didType = (DidType)Convert.ToInt32(type);

        var indexDerivation = didType switch 
        {
            DidType.CardanoPayment => currentPaymentCount,
            DidType.Simple => currentSimpleCount,
            _ => 0,
        };

        Did did = new Did()
        {
            DidType = (int)didType,
            KeyId = SelectedKey.Id,
            IndexDerivation = indexDerivation,
        };

        await Shell.Current.GoToAsync(nameof(PickResolutionTypeView), new Dictionary<string, object>() { { "NewDid", did } });
    }
}
