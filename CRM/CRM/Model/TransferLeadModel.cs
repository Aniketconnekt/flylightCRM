using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    class TransferLeadModel
    {
    }


    public class CampUsers
    {
        public bool Result { get; set; }
        public CampUsersData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }

    public class CampUsersData
    {
        public int Id { get; set; }
        public object RoleId { get; set; }
        public string UserName { get; set; }
        public object DOB { get; set; }
        public object MobileNumber { get; set; }
        public object Password { get; set; }
        public object AlternateNo { get; set; }
        public object Email { get; set; }
        public object Company { get; set; }
        public object GSTNo { get; set; }
        public object CampaignId { get; set; }
        public object Address { get; set; }
        public object CityId { get; set; }
        public object StateId { get; set; }
        public object District { get; set; }
        public object StatusId { get; set; }
        public object CreationDate { get; set; }
        public object CreatedById { get; set; }
        public object ModifiedById { get; set; }
        public object ModificationDate { get; set; }
        public object UserType { get; set; }
        public DateTime ExpiryDate { get; set; }
        public object CityName { get; set; }
        public object StateName { get; set; }
    }

}
