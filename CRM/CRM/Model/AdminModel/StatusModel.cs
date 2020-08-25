using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.AdminModel
{
    public class StatusModel
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public string CreatedById { get; set; }
    }

    public class GetStatus
    {
        public bool Result { get; set; }
        public StatusData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }


    public class AddUpdateStatusResponse
    {
        public bool Result { get; set; }
        public StatusData Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public object RelativeUrl { get; set; }
    }

    public class StatusData
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public DateTime? CreationDate { get; set; }
    }
}
