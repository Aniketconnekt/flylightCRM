using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class ContactModel
    {
        public string Name { get; set; }
        public string[] Emails { get; set; }
        public string[] PhoneNumbers { get; set; }
    }
}
