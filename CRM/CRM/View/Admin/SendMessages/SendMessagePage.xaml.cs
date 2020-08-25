using CRM.ViewModel.AdminViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Admin.SendMessages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SendMessagePage : ContentPage
	{
        SendMessageViewModel _sendMessageViewModel;
        public SendMessagePage ()
		{
			InitializeComponent ();
            BindingContext = _sendMessageViewModel = new SendMessageViewModel(Navigation);
        }
	}
}