using System;

namespace CRM.Model.AdminModel
{
    public class LeadDetailsModel
    {
        public bool Result { get; set; }
        public LeadDetailsData Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public object RelativeUrl { get; set; }
        public int TotalCount { get; set; }
    }

    public class LeadDetailsData
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public LeadDetailsList[] CalledList { get; set; }
        public LeadDetailsList[] PendingList { get; set; }
    }
    public class LeadDetailsList
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
        public string CreationDate { get; set; }
        public int ModifiedById { get; set; }
        public string ModificationDate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CurrentStatus { get; set; }
        public object GSTNO { get; set; }
        public object DOB { get; set; }
        public object CampaignName { get; set; }
        public string LeadId { get; set; }
        public string ActionDate { get; set; }
        public string FollowupTime { get; set; }
        public int ActionId { get; set; }
        public object Status { get; set; }
        public object Description { get; set; }
    }
}
