using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.SuperAdmin.Model
{
    public class CampaignsModel
    {
        public bool Result { get; set; }
        public CampaignsData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }

    public class CampaignsData
    {
        public string Campaign { get; set; }
        public int TotalLeads { get; set; }
        public int VisitedLeads { get; set; }
        public int PendingLeads { get; set; }
    }

}
