using System;
using System.Collections.Generic;
using System.Linq;
using CRM.CustomControls;
using CRM.SuperAdmin.Model;
using CRM.SuperAdmin.View.Performance;
using CRM.SuperAdmin.ViewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.SuperAdmin.View.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        MasterDetailViewModel viewModel;
        int last_selected_menu = 1;
        public MasterDetailView()
        {
            try
            {
                InitializeComponent();
                BindingContext = viewModel = new MasterDetailViewModel(Navigation);
                Master = masterPage;
                App.MasterDetailPage = this;
                Detail = new NavigationPage(new CRM.SuperAdmin.View.Home.HomePage())
                {
                    BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                    BarTextColor = Color.FromHex(App.nav_bar_text_color),
                };
                masterPage.ListView.ItemsSource = viewModel.list;
                masterPage.ListView.ItemSelected += menu_list_ItemSelected;
                MasterBehavior = MasterBehavior.Popover;
            }
            catch (Exception ex)
            {
            }
        }

        private async void menu_list_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                var item = e.SelectedItem as MasterDetailModel;
                if(item==null)
                    return;

                var last_selected_item = viewModel.list.FirstOrDefault(x=>x.id ==last_selected_menu);
                if (last_selected_item!= null)
                {
                    last_selected_item.row_background_color= "#FFFFFF";
                    last_selected_item.title_text_color = "#4C4C4C";
                    last_selected_item.icon= SetLastIcon(last_selected_item.title);
                }
                IsPresented = false;
                switch (item.title)
                {
                    case "Home":
                        last_selected_menu = 1;
                        item.icon = "ic_address_active.png";
                        item.title_text_color = "#2BAAE1";
                        item.row_background_color = "#FAFAFA";
                        Detail = new NavigationPage(new CRM.SuperAdmin.View.Home.HomePage())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        }; ;
                        break;
                    case "Add Admin/Customer":
                        last_selected_menu = 2;
                        item.icon = "ic_menu_add_user_blue.png";
                        item.title_text_color = "#2BAAE1";
                        item.row_background_color = "#FAFAFA";
                        Detail = new NavigationPage(new AddAdminCustomer.AddAdminCustomerPage(null))
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        }; 
                        break;
                    case "Performance":
                        last_selected_menu = 3;
                        item.icon = "ic_menu_performance.png";
                        item.title_text_color = "#2BAAE1";
                        item.row_background_color = "#FAFAFA";
                        Detail = new NavigationPage(new PerformancePage())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        break;
                    case "Share App":
                        last_selected_menu = 4;
                        item.icon = "ic_share.png";
                        item.title_text_color = "#2BAAE1";
                        item.row_background_color = "#FAFAFA";
                        await Share.RequestAsync(new ShareTextRequest
                        {
                            Uri = "https://play.google.com/store/apps/details?id=in.connekt.flylight",
                            Title = "Share FlylightCRM"
                        });
                        break;
                    case "Contact Us":
                        last_selected_menu = 5;
                        item.icon = "ic_call.png";
                        item.title_text_color = "#2BAAE1";
                        item.row_background_color = "#FAFAFA";
                        Detail = new NavigationPage(new CRM.View.ContactUs.ContactUsPage())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        break;
                    case "Logout":
                        last_selected_menu = 6;
                        item.icon = "ic_menu_inactive_language.png";
                        item.title_text_color = "#2BAAE1";
                        item.row_background_color = "#FAFAFA";
                        DependencyService.Get<IFileService>().deleteFile();
                        Application.Current.MainPage = new NavigationPageGradientHeader(new CRM.View.Login.LoginPage())
                        {
                            LeftColor = Color.FromHex("#2699ca"),
                            RightColor = Color.FromHex("#2baae1")
                        };
                        break;
                }

                masterPage.ListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
            }
        }

        public string SetLastIcon(string page_name)
        {
            try
            {
                string last_icon = string.Empty;
                switch (page_name)
                {
                    case "Home":
                        last_icon = "ic_address.png";
                        break;
                    case "Add Admin/Customer":
                        last_icon = "ic_menu_inactive_add_lead.png";
                        break;
                    case "Performance":
                        last_icon = "ic_menu_inactive_performance.png";
                        break;
                    case "Share App":
                        last_icon = "ic_share_inactive.png";
                        break;
                    case "Contact Us":
                        last_icon = "ic_call_inactive.png";
                        break;
                    case "Logout":
                        last_icon = "ic_menu_inactive_language.png";
                        break;
                }
                return last_icon;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}