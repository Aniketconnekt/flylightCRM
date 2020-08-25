using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Successfull
{
    [XamlCompilation(XamlCompilationOptions.Compile)]   
    public partial class SuccessfullPage : ContentPage
    {
        public SuccessfullPage()
        {
            InitializeComponent();
            ImgSuccessful.Source ="ic_successful";
        }

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new NavigationPage(new Lead.LeadPage())
            //{
            //    BarBackgroundColor = Color.FromHex(App.nav_bar_color),
            //    BarTextColor = Color.FromHex(App.nav_bar_text_color),
            //});
            Application.Current.MainPage = new NavigationPage(new View.Menu.MasterDetailView())
            {
                BarBackgroundColor = Color.FromHex("#2699ca"),
                BarTextColor = Color.FromHex("#FFFFFF"),
            };
        }
    }
}