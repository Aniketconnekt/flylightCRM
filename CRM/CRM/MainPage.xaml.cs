using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri("whatsapp://send?phone=+9877771549"));
                //Chat.Open("+5515999999999", "Send this message");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}
