using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class AddUserLeadModel
    {
        public int Id { get; set; }
        public int CampaignId { get; set; }
        public string Name { get; set; }
        public string DOB { get; set; }
        public string Company { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int CityId { get => 1; }
        public string District { get; set; }
        public int StateId { get => 1; }
        public string State { get; set; }
        public string Email { get; set; }
        public string CreatedById { get; set; }
        public string UserId { get; set; }
    }
}
