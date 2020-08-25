using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.AdminModel
{
    public class AddCampaignModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedById { get; set; }
    }

    public class AddCampaignResponse
    {
        public bool Result { get; set; }
        public AddCampaignData Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public object RelativeUrl { get; set; }
    }

    public class AddCampaignData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object StatusId { get; set; }
        public object CreationDate { get; set; }
        public int CreatedById { get; set; }
        public object ModifiedById { get; set; }
        public object ModificationDate { get; set; }
        public object CampaignCode { get; set; }
        public string Description { get; set; }
    }
}
