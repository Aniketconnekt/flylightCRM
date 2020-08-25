using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Successful
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Successful : ContentPage
    {
        public Successful()
        {
            InitializeComponent();
        }

        private void btnSend_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new View.Menu.MasterDetailView())
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color),
            };
        }
    }
}