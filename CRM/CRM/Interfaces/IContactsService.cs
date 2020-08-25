using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CRM.Interfaces
{
    public class ContactEventArgs : EventArgs
    {
        public ContactModel Contact { get; }
        public ContactEventArgs(ContactModel contact)
        {
            Contact = contact;
        }
    }

    public interface IContactsService
    {
        event EventHandler<ContactEventArgs> OnContactLoaded;
        bool IsLoading { get; }
        Task<IList<ContactModel>> RetrieveContactsAsync(CancellationToken? token = null);
    }
}
