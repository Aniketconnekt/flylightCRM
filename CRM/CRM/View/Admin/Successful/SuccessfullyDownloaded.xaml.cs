using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.Successful
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuccessfullyDownloaded : ContentPage
    {
        public SuccessfullyDownloaded()
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