using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.SuperAdmin.Model
{
    public class HomeModel
    {
        public bool Result { get; set; }
        public HomeData Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public object RelativeUrl { get; set; }
    }

    public class HomeData
    {
        public int UserCount { get; set; }
        public int ClientCount { get; set; }
        public int LocalSearchedCount { get; set; }
        public int RoleId { get; set; }
        public bool IsUpdateProfile { get; set; }
        public int TotalLead { get; set; }
    }

}
