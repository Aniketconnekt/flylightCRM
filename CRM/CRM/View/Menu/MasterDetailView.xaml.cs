using CRM.Common.Helpers;
using CRM.CustomControls;
using CRM.Model;
using CRM.View.Admin.Users;
using CRM.View.LeadSelection;
using System;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CRM.View.Menu
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterDetailView : MasterDetailPage
    {
        CRM.ViewModel.MasterDetailViewModel viewModel;
        int last_selected_menu = 1;

        public MasterDetailView()
        {
            try
            {
                InitializeComponent();
                viewModel = new CRM.ViewModel.MasterDetailViewModel(Navigation);
                Master = masterPage;
                App.MasterDetailPage = this;
                var roleId = Settings.CRM_UserRole;//SecureStorage.GetAsync(AppConstant.UserRole);
                if (roleId == "2")
                {
                    App.nav_bar_text_color = "#FFFFFF";
                    App.nav_bar_color = "#2baae1";
                    App.menu_selected_text_color = "#2baae1";
                    Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Admin.Lead.Lead)))
                    {
                        BarBackgroundColor = Color.FromHex("#2baae1"),
                        BarTextColor = Color.FromHex("#FFFFFF")
                    };
                    BackgroundImageSource = "background.png";
                }
                else
                {
                    App.nav_bar_text_color = "#FFFFFF";
                    App.nav_bar_color = "#2699ca";
                    App.menu_selected_text_color = "#2699ca";
                    Detail = new NavigationPage((Page)Activator.CreateInstance(typeof(Lead.LeadPage)))
                    {
                        BarBackgroundColor = Color.FromHex("#2699ca"),
                        BarTextColor = Color.FromHex("#FFFFFF")
                    };
                    BackgroundImageSource = "background.png";
                }
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
                if (item == null)
                    return;

                IsPresented = false;
                var roleId = Settings.CRM_UserRole; //await SecureStorage.GetAsync(AppConstant.UserRole);
                switch (item.title)
                {
                    case "Dashboard":
                        item.icon = roleId == "2" ? "ic_menu_add_lead.png" : "ic_menu_add_lead.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        if (roleId == "2")
                        {
                            Detail = new NavigationPage(new Admin.Lead.Lead())
                            {
                                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                                BarTextColor = Color.FromHex(App.nav_bar_text_color),
                            };
                        }
                        else
                        {
                            Detail = new NavigationPage(new Lead.LeadPage())
                            {
                                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                                BarTextColor = Color.FromHex(App.nav_bar_text_color),
                            };
                        }
                        ResetMenu(item);
                        break;
                    case "Campaigns":
                        item.icon = "ic_menu_campaign_blue.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new Admin.Compaign.Campaigns())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Add Users":
                        item.icon = "ic_menu_add_user_blue.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new AddUser(null))
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Leads":
                        item.icon = roleId == "2" ? "ic_menu_add_lead.png" : "ic_menu_add_lead.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        if (roleId == "2")
                        {
                            Detail = new NavigationPage(new View.Admin.Lead.Leads())
                            {
                                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                                BarTextColor = Color.FromHex(App.nav_bar_text_color),
                            };
                        }
                        else
                        {
                            Detail = new NavigationPage(new LeadSelectionPage(null))
                            {
                                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                                BarTextColor = Color.FromHex(App.nav_bar_text_color),
                            };
                        }

                        ResetMenu(item);
                        break;
                    case "Allotted Leads":
                        item.icon = roleId == "2" ? "ic_menu_allotted_lead.png" : "ic_menu_allotted_lead.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new LeadAllotted.LeadAllottedPage(null))
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Lead Allottment":
                        item.icon = "ic_menu_leadallotment_blue.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new View.Admin.Lead.LeadAllottment())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Follow UP":
                        item.icon = "ic_menu_followup.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new FollowUp.FollowUpPage())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Transfer Leads":
                        item.icon = "ic_transfer.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new Admin.TransferLeads.TransferLeadPage())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Performance":
                        item.icon = roleId == "2" ? "ic_menu_performance.png" : "ic_menu_performance.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        if (roleId == "2")
                        {
                            Detail = new NavigationPage(new View.Admin.Performance.Performance())
                            {
                                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                                BarTextColor = Color.FromHex(App.nav_bar_text_color),
                            };
                        }
                        else
                        {
                            Detail = new NavigationPage(new Performance.PerformancePage())
                            {
                                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                                BarTextColor = Color.FromHex(App.nav_bar_text_color),
                            };
                        }
                        ResetMenu(item);
                        break;
                    case "Lead History":
                        item.icon = "ic_menu_history.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new LeadHistory.LeadHistoryPage())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Add Status":
                        item.icon = "ic_addstatus_active.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new Admin.Status.StatusPage())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Send Message":
                        item.icon = "ic_email_active.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new Admin.SendMessages.SendMessagePage())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Language Settings":
                        item.icon = "ic_menu_language.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new View.SelectLanguage.SelectLanguagePage(String.Empty))
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Share App":
                        item.icon = "ic_share.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        await Share.RequestAsync(new ShareTextRequest
                        {
                            Uri = "https://play.google.com/store/apps/details?id=in.connekt.flylight",
                            Title = "Share FlylightCRM"
                        });
                        ResetMenu(item);
                        break;
                    case "Contact Us":
                        item.icon = "ic_call.png";
                        item.title_text_color = App.menu_selected_text_color;
                        item.seperator_visible = true;
                        Detail = new NavigationPage(new View.ContactUs.ContactUsPage())
                        {
                            BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                            BarTextColor = Color.FromHex(App.nav_bar_text_color),
                        };
                        ResetMenu(item);
                        break;
                    case "Logout":
                        item.icon = "ic_menu_inactive_language.png";
                        item.seperator_visible = true;
                        DependencyService.Get<IFileService>().deleteFile();
                        Application.Current.MainPage = new NavigationPageGradientHeader(new Login.LoginPage())
                        {
                            LeftColor = Color.FromHex("#2699ca"),
                            RightColor = Color.FromHex("#2baae1")
                        };
                        ResetMenu(item);
                        break;
                }
                masterPage.ListView.SelectedItem = null;
            }
            catch (Exception ex)
            {
            }
        }

        private async void ResetMenu(MasterDetailModel selectedMenu)
        {
            var userId = await SecureStorage.GetAsync("UserId");
            foreach (var item in viewModel.list)
            {
                if (item.id != selectedMenu.id)
                {
                    item.seperator_visible = false;
                    item.title_text_color = "#4C4C4C";
                    if (item.icon == "ic_menu_campaign_blue.png")
                        item.icon = "ic_menu_inactive_campaign.png";
                    else if (item.icon == "ic_menu_leadallotment_blue.png")
                        item.icon = "ic_menu_inactive_leadallotment.png";
                    else if (item.icon == "ic_menu_add_lead.png" || item.icon == "ic_menu_add_lead.png")
                        item.icon = "ic_menu_inactive_add_lead.png";
                    else if (item.icon == "ic_menu_allotted_lead.png" || item.icon == "ic_menu_allotted_lead.png")
                        item.icon = "ic_menu_inactive_allotted_lead.png";
                    else if (item.icon == "ic_menu_followup.png")
                        item.icon = "ic_menu_inactive_followup.png";
                    else if (item.icon == "ic_menu_performance.png" || item.icon == "ic_menu_performance.png")
                        item.icon = "ic_menu_inactive_performance.png";
                    else if (item.icon == "ic_menu_history.png" || item.icon == "ic_menu_history.png")
                        item.icon = "ic_menu_inactive_history.png";
                    else if (item.icon == "ic_menu_language.png" || item.icon == "ic_menu_language.png")
                        item.icon = "ic_menu_inactive_language.png";
                    else if (item.icon == "ic_menu_logout.png")
                        item.icon = "ic_menu_inactive_logout.png";
                    else if (item.icon == "ic_menu_add_user_blue.png")
                        item.icon = "ic_menu_inactive_add_user.png";
                    else if (item.icon == "ic_addstatus_active.png")
                        item.icon = "ic_addstatus.png";
                    else if (item.icon == "ic_share.png")
                        item.icon = "ic_share_inactive.png";
                    else if (item.icon == "ic_call.png")
                        item.icon = "ic_call_inactive.png";
                    else if (item.icon == "ic_transfer.png")
                        item.icon = "ic_transfer_inactive.png";
                    else if (item.icon == "ic_email_active.png")
                        item.icon = "ic_email.png";
                }
                else
                {
                    item.seperator_visible = true;
                    item.title_text_color = App.menu_selected_text_color;
                    if (item.icon == "ic_menu_inactive_add_lead.png")
                        item.icon = userId == "2" ? "ic_menu_add_lead.png" : "ic_menu_add_lead.png";
                    else if (item.icon == "ic_menu_inactive_allotted_lead.png")
                        item.icon = "ic_menu_allotted_lead.png";
                    else if (item.icon == "ic_menu_inactive_followup.png")
                        item.icon = "ic_menu_followup.png";
                    else if (item.icon == "ic_menu_inactive_performance.png")
                        item.icon = userId == "2" ? "ic_menu_performance.png" : "ic_menu_performance.png";
                    else if (item.icon == "ic_menu_inactive_history.png")
                        item.icon = userId == "2" ? "ic_menu_history.png" : "ic_menu_history.png";
                    else if (item.icon == "ic_menu_inactive_language.png")
                        item.icon = "ic_menu_language.png";
                    else if (item.icon == "ic_menu_inactive_logout.png")
                        item.icon = "ic_menu_logout.png";
                    if (item.icon == "ic_menu_inactive_campaign.png")
                        item.icon = "ic_menu_campaign_blue.png";
                    else if (item.icon == "ic_menu_inactive_leadallotment.png")
                        item.icon = "ic_menu_leadallotment_blue.png";
                    else if (item.icon == "ic_menu_inactive_add_user.png")
                        item.icon = "ic_menu_add_user_blue.png";
                    else if (item.icon == "ic_addstatus.png")
                        item.icon = "ic_addstatus_active.png";
                    else if (item.icon == "ic_share_inactive.png")
                        item.icon = "ic_share.png";
                    else if (item.icon == "ic_call_inactive.png")
                        item.icon = "ic_call.png";
                    else if (item.icon == "ic_transfer_inactive.png")
                        item.icon = "ic_transfer.png";
                    else if (item.icon == "ic_email.png")
                        item.icon = "ic_email_active.png";
                }
            }
            masterPage.ListView.ItemsSource = new ObservableCollection<MasterDetailModel>(viewModel.list);
        }

        //public string SetLastIcon(string page_name)
        //{
        //    try
        //    {
        //        string last_icon = string.Empty;
        //        switch (page_name)
        //        {
        //            case "Dashboard":
        //                last_icon = "ic_menu_inactive_add_lead.png";
        //                break;
        //            case "Add Leads":
        //                last_icon = "ic_menu_inactive_add_lead.png";
        //                break;
        //            case "Allotted Leads":
        //                last_icon = "ic_menu_inactive_allotted_lead.png";
        //                break;
        //            case "Follow UP":
        //                last_icon = "ic_menu_inactive_followup.png";
        //                break;
        //            case "Performance":
        //                last_icon = "ic_menu_inactive_performance.png";
        //                break;
        //            case "Lead History":
        //                last_icon = "ic_menu_inactive_history.png";
        //                break;
        //            case "Language Settings":
        //                last_icon = "ic_menu_inactive_language.png";
        //                break;
        //            case "Logout":
        //                last_icon = "ic_menu_inactive_language.png";
        //                break;
        //        }
        //        return last_icon;
        //    }
        //    catch (Exception ex)
        //    {
        //        return "";
        //    }
        //}

    }
}