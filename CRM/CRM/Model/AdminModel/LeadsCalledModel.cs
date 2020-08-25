using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.AdminModel
{
    public class LeadsCalledModel
    {
        public bool Result { get; set; }
        public LeadsCalledData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }

    public class LeadsCalledData
    {
        public string Username { get; set; }
        public string MobileNo { get; set; }
        public int UserId { get; set; }
        public int AllottedTotalLead { get; set; }
        public int CalledLead { get; set; }
        public int PendingLeads { get; set; }
    }

}
