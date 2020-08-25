using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.SuperAdmin.Model
{
    public class AdminCustomerModel
    {
        public bool Result { get; set; }
        public AdminCustomerData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }

    public class AdminCustomerData
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string UserName { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string AlternateNo { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string GSTNo { get; set; }
        public int? CampaignId { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public string CityName { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public string District { get; set; }
        public int StatusId { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedById { get; set; }
        public int ModifiedById { get; set; }
        public DateTime ModificationDate { get; set; }
    }
}
