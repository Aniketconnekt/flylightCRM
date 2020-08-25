using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.AdminModel
{
    public class LeadsModel
    {
        public bool Result { get; set; }
        public LeadsData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }
    }

    public class LeadsData : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
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
        public int CreatedById { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModificationDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public object CurrentStatus { get; set; }
        public string GSTNO { get; set; }
        public string DOB { get; set; }
        public string CampaignName { get; set; }
        public int LeadId { get; set; }
        public DateTime ActionDate { get; set; }
        public string FollowupTime { get; set; }
        public int ActionId { get; set; }
        public object Status { get; set; }
        public object Description { get; set; }

        private bool _isSelectAllChecked = false;
        public bool IsSelectAllChecked
        {
            get => _isSelectAllChecked;
            set { _isSelectAllChecked = value; OnPropertyChanged(); }
        }
    }

}
