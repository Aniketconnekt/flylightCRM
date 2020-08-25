using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.AdminModel
{
    public class LeadModel
    {
        public bool Result { get; set; }
        public LeadData Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public object RelativeUrl { get; set; }
    }

    public class LeadData
    {
        public int ActionTaken { get; set; }
        public int Success { get; set; }
        public int TotalLeads { get; set; }
        public int TotalUsers { get; set; }
        public int FollowUp { get; set; }
    }

}
