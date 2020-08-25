using CRM.Common.Command;
using CRM.Interfaces;
using CRM.Model;
using CRM.View.Admin.Lead;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CRM.ViewModel.AdminViewModel
{
    public class ContactsViewModel : BaseViewModel
    {
        #region CTOR
        public ContactsViewModel(INavigation navigation) : base(navigation)
        {
            Contacts = new ObservableCollection<ContactModel>();
            BindingBase.EnableCollectionSynchronization(Contacts, null, ObservableCollectionCallback);
            App.ContactsService.OnContactLoaded += OnContactLoaded;
            LoadContacts();
        }
        #endregion

        #region Commands
        public DelegateCommand SelectContactCommand => new DelegateCommand(ExecuteOnSelectContact);
        #endregion

        #region Properties
        public ObservableCollection<ContactModel> Contacts { get; set; }
        public ObservableCollection<ContactModel> FilteredContacts { get { return Contacts; } }
        #endregion

        #region Methods
        void ObservableCollectionCallback(IEnumerable collection, object context, Action accessMethod, bool writeAccess)
        {
            // `lock` ensures that only one thread access the collection at a time
            lock (collection)
            {
                accessMethod?.Invoke();
            }
        }
        private void OnContactLoaded(object sender, ContactEventArgs e)
        {
            Contacts.Add(e.Contact);
        }
        async Task LoadContacts()
        {
            try
            {
                await App.ContactsService.RetrieveContactsAsync();
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Task was cancelled");
            }
        }
        public void ExecuteOnSelectContact(object obj)
        {
            var contact = obj as ContactModel;
            App.MasterDetailPage.Detail = new NavigationPage(new AddLead(contact, String.Empty))
            {
                BarBackgroundColor = Color.FromHex(App.nav_bar_color),
                BarTextColor = Color.FromHex(App.nav_bar_text_color),
            };
        }
        #endregion
    }
}
