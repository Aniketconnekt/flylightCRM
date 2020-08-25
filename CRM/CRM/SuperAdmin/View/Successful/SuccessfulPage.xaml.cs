using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.SuperAdmin.View.Successful
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SuccessfulPage : ContentPage
	{
		public SuccessfulPage ()
		{
			InitializeComponent ();
		}

        private void btnSend_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new SuperAdmin.View.Menu.MasterDetailView())
            {
                BarBackgroundColor = Color.FromHex("#2699ca"),
                BarTextColor = Color.FromHex("#FFFFFF")
            };
        }
    }
}