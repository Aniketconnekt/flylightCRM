using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.ViewModel
{
    public class LADCountData
    {
        public int AllottedTotalLead { get; set; }
       
        public int CalledLead { get; set; }

        public int PendingLeads { get; set; }
    }

    public class DashboardLADCountModel
    {
        public string Result { get; set; }
    
        public LADCountData Object { get; set; }
      
        public string Message { get; set; }
      
        public string ChangeLocation { get; set; }
        
        public string RelativeUrl { get; set; }
    }

}
