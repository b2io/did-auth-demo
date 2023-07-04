using DidAuthDemo.Maui.Views.CredentialViews;
using DidAuthDemo.Maui.Views.DidDocumentViews;
using DidAuthDemo.Maui.Views.KeyViews;

namespace DidAuthDemo.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(DidDocumentListView), typeof(DidDocumentListView));
            Routing.RegisterRoute(nameof(DidDocumentDetailView), typeof(DidDocumentDetailView));
            Routing.RegisterRoute(nameof(DidDocumentCreateView), typeof(DidDocumentCreateView));
            Routing.RegisterRoute(nameof(PickDocumentTypeListView), typeof(PickDocumentTypeListView));
            Routing.RegisterRoute(nameof(PickResolutionTypeView), typeof(PickResolutionTypeView));

            Routing.RegisterRoute(nameof(MnemonicInfoView), typeof(MnemonicInfoView));
            Routing.RegisterRoute(nameof(ShowMnemonicView), typeof(ShowMnemonicView));
            Routing.RegisterRoute(nameof(KeyDetailView), typeof(KeyDetailView));
            Routing.RegisterRoute(nameof(KeyListView), typeof(KeyListView));

            Routing.RegisterRoute(nameof(CredentialListView), typeof(CredentialListView));
        }
    }
}