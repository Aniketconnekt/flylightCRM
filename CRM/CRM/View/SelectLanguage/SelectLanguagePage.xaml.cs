using CRM.View.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.SelectLanguage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectLanguagePage : ContentPage
    {
        string _type = String.Empty;
        public SelectLanguagePage(string type)
        {
            InitializeComponent();
            _type = type;
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(_type))
                this.Navigation.PushAsync(new Login.LoginPage());
            else
                Application.Current.MainPage = new NavigationPage(new MasterDetailView())
                {
                    BarBackgroundColor = Color.FromHex("#2699ca"),
                    BarTextColor = Color.FromHex("#FFFFFF"),
                };
        }
    }
}