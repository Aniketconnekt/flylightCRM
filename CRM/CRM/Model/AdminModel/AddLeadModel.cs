using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model.AdminModel
{
    public class AddLeadModel
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Company { get; set; }
        public string GSTNO { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateNo { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int CityId { get => 1; }
        public string District { get; set; }
        public int StateId { get => 1; }
        public string State { get; set; }
        public string Email { get; set; }
        public string CreatedById { get; set; }
        public int UserId { get => 1; }
    }
}
