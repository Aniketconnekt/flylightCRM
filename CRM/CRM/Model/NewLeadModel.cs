using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class NewLeadModel
    {
        public bool Result { get; set; }
        public NewLeadData Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public object RelativeUrl { get; set; }
    }
    public class NewLeadData
    {
        public object Username { get; set; }
        public object MobileNo { get; set; }
        public int UserId { get; set; }
        public int AllottedTotalLead { get; set; }
        public int CalledLead { get; set; }
        public int PendingLeads { get; set; }
        public int FollowUp { get; set; }
    }
    public class Leads
    {
        public bool Result { get; set; }
        public NewLeadsData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }
    }

    public class NewLeadsData : BaseModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateNo { get; set; }
        public string Company { get; set; }
        public int CampaignId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public object CityId { get; set; }
        public object StateId { get; set; }
        public string District { get; set; }
        public int StatusId { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModificationDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CurrentStatus { get; set; }
        public object GSTNO { get; set; }
        public string DOB { get; set; }
        public string CampaignName { get; set; }
        public string CampDescription { get; set; }
        public int LeadId { get; set; }
        public string ActionDate { get; set; }
        public TimeSpan FollowupTime { get; set; }
        public int ActionId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string LeadFollowuptime { get; set; }

        private bool _isSelectAllChecked = false;
        public bool IsSelectAllChecked
        {
            get => _isSelectAllChecked;
            set { _isSelectAllChecked = value; OnPropertyChanged(); }
        }
    }
}
