using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class CreditFormModel
    {
        public string CreatedById { get; set; }
        public string CreditAmount { get; set; }
        public int LeadId { get; set; }
        public string Desciption { get; set; }
    }
}
