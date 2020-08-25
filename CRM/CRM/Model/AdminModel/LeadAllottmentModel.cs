using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.AdminModel
{
    public class LeadAllottmentModel
    {
        public bool Result { get; set; }
        public LeadAllottmentData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }

    public class LeadAllottmentData : BaseModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Company { get; set; }
        public string CampaignId { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public string StateId { get; set; }
        public string District { get; set; }
        public string StatusId { get; set; }
        public string CreatedById { get; set; }
        public DateTime CreationDate { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModificationDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CurrentStatus { get; set; }
        public string GSTNO { get; set; }
        public string DOB { get; set; }
        public string LeadId { get; set; }
        public DateTime ActionDate { get; set; }
        public string FollowupTime { get; set; }
        public string ActionId { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }

        private bool _isSelectAllChecked = false;
        public bool IsSelectAllChecked
        {
            get => _isSelectAllChecked;
            set { _isSelectAllChecked = value; OnPropertyChanged(); }
        }
    }
}
