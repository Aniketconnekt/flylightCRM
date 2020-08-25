using CRM.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.ContactUs
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactUsPage : ContentPage
	{
        ContactUsViewModel _contactUsViewModel;
        public ContactUsPage ()
		{
			InitializeComponent ();
            BindingContext = _contactUsViewModel = new ContactUsViewModel(Navigation);
        }
	}
}