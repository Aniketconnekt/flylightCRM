using CRM.Common.Command;
using CRM.View.Login;
using CRM.View.Menu;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CRM.ViewModel
{
    public class SelectLanguageViewModel : BaseViewModel
    {
        string _type = String.Empty;

        #region CTOR
        public SelectLanguageViewModel(INavigation navigation) : base(navigation)
        {
        }
        #endregion

        #region Commands
        public DelegateCommand LanguageSelectedCommand => new DelegateCommand(ExecuteOnLanguageSelected);
        #endregion

        #region Properties
        private List<string> _languagesList;
        public List<string> LanguagesList
        {
            get => _languagesList;
            set { _languagesList = value; OnPropertyChanged(); }
        }

        private string _selectedLanguage;
        public string SelectedLanguage
        {
            get => _selectedLanguage;
            set { _selectedLanguage = value; OnPropertyChanged(); }
        }
        #endregion

        #region Methods
        public void Initialize(string type)
        {
            _type = type;
            LanguagesList = new List<string>() { "ENGLISH", "हिंदी" };
        }
        private async void ExecuteOnLanguageSelected(object obj)
        {
            if (!string.IsNullOrWhiteSpace(_type))
                await _navigation.PushAsync(new LoginPage());
            else
                Application.Current.MainPage = new NavigationPage(new MasterDetailView())
                {
                    BarBackgroundColor = Color.FromHex("#2699ca"),
                    BarTextColor = Color.FromHex("#FFFFFF"),
                };
        }
        #endregion
    }
}
