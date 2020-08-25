using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.AdminModel
{
    public class PerformanceModel
    {
        public bool Result { get; set; }
        public PerformanceData Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public object RelativeUrl { get; set; }
    }

    public class PerformanceData
    {
        public int LeadsCalled { get; set; }
        public int Leadspending { get; set; }
        public int TotalLeads { get; set; }
        public int LeadsUnCalled { get; set; }
    }

}
