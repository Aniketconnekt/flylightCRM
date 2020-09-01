using CRM.Common.Helpers;
using CRM.Interfaces;
using CRM.Model;
using Xamarin.Forms;


namespace CRM
{
    public partial class App : Application
    {
        public static string nav_bar_color = "#2699ca";
        public static string nav_bar_text_color = "#FFFFFF";
        public static string menu_selected_text_color = string.Empty;
        //public static string user_role = string.Empty;
        public static MasterDetailPage MasterDetailPage;
        public static LoginObject LoginUserDetails = new LoginObject();
        public static IContactsService ContactsService;

        public App(IContactsService contactsService)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjYxMzYwQDMxMzgyZTMxMmUzMGlRM3VMcUkrZzVKU0FjY3FISkoyODFLUGhJR0NSenk3N2tuZ3h1NlplajA9");
            InitializeComponent();
            ContactsService = contactsService;
            if (DependencyService.Get<IFileService>().checkExists())
            {
                var userRole = Settings.CRM_UserRole; //SecureStorage.GetAsync(AppConstant.UserRole);
                if (userRole == "1")
                {
                    App.nav_bar_text_color = "#FFFFFF";
                    App.nav_bar_color = "#2699ca";
                    MainPage = new NavigationPage(new SuperAdmin.View.Menu.MasterDetailView())
                    {
                        BarBackgroundColor = Color.FromHex("#2699ca"),
                        BarTextColor = Color.FromHex("#FFFFFF"),
                    };
                }
                else
                {
                    if (userRole == "2")
                    {
                        App.nav_bar_color = "#2baae1";
                        App.menu_selected_text_color = "#2baae1";
                    }
                    MainPage = new View.Menu.MasterDetailView();
                }
            }
            else
                MainPage = new NavigationPage(new View.SelectLanguage.SelectLanguagePage("StartApp"));
        }

        public App()
        {
            InitializeComponent();
            if (DependencyService.Get<IFileService>().checkExists())
            {
                var userRole = Settings.CRM_UserRole; //SecureStorage.GetAsync(AppConstant.UserRole);
                if (userRole == "1")
                {
                    App.nav_bar_text_color = "#FFFFFF";
                    App.nav_bar_color = "#2699ca";
                    MainPage = new NavigationPage(new SuperAdmin.View.Menu.MasterDetailView())
                    {
                        BarBackgroundColor = Color.FromHex("#2699ca"),
                        BarTextColor = Color.FromHex("#FFFFFF"),
                    };
                }
                else
                {
                    if (userRole == "2")
                    {
                        App.nav_bar_color = "#2baae1";
                        App.menu_selected_text_color = "#2baae1";
                    }
                    MainPage = new View.Menu.MasterDetailView();
                }
            }
            else
                MainPage = new NavigationPage(new View.SelectLanguage.SelectLanguagePage("StartApp"));
        }

        protected override void OnStart()
        {
            IFirebaseAnalyticsService service = DependencyService.Get<IFirebaseAnalyticsService>();
            service?.LogEvent("MyEventHere");
        }
        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}