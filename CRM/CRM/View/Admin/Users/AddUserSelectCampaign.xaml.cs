using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Users
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddUserSelectCampaign : ContentPage
    {
        public AddUserSelectCampaign()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
        }


        private void btnSkip_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new View.Menu.MasterDetailView())
            {
                BarBackgroundColor = Color.FromHex("#2699ca"),
                BarTextColor = Color.FromHex("#FFFFFF"),
            };
        }

        private async void Allot_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new View.Admin.Successful.Successful());
        }
    }
}