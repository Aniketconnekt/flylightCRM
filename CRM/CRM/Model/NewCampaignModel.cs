using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class NewCampaignModel
    {
        public bool Result { get; set; }
        public NewCampaignData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }
    public class NewCampaignData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; }
        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModificationDate { get; set; }
        public object CampaignCode { get; set; }
        public string Description { get; set; }
        public object[] Camplist { get; set; }
    }

    public class CampaignAction
    {
        public bool Result { get; set; }
        public CampaignActionData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }
    public class CampaignActionData
    {
        public int Id { get; set; }
        public string ActionName { get; set; }
        public DateTime ActionDate { get; set; }
        public int StatusId { get; set; }
        public int CreatedById { get; set; }
        public int ModifiedById { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public object[] ActionList { get; set; }
    }



    //public class CampaignList
    //{
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public int StatusId { get; set; }
    //    public DateTime CreationDate { get; set; }
    //    public int CreatedById { get; set; }
    //    public int ModifiedById { get; set; }
    //    public DateTime ModificationDate { get; set; }
    //    public int? CampaignCode { get; set; }
    //    public string Description { get; set; }
    //    public IList<string> Camplist { get; set; }

    //}
    //public class NewCampaignModel
    //{
    //    public bool Result { get; set; }
    //    public List<CampaignList> List { get; set; }
    //    public int Count { get; set; }
    //    public IList<object> IList { get; set; }
    //    public string Message { get; set; }

    //}
}
