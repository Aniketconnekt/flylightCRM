using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.SuperAdmin.Model
{
    public class MoreDetailModel
    {
        public bool Result { get; set; }
        public MoreDetailData Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public object RelativeUrl { get; set; }
    }

    public class MoreDetailData
    {
        public string AdminName { get; set; }
        public string TotalCampaign { get; set; }
        public string TotalUsers { get; set; }
        public string TotalLeads { get; set; }
    }

}
