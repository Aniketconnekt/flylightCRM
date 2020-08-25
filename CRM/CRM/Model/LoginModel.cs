using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Model
{
    public class LoginObject
    {
        public int Id { get; set; }
        public string RoleId { get; set; }
        public string UserName { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string Password { get; set; }
        public string AlternateNo { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string GSTNo { get; set; }
        public string CampaignId { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public string StateId { get; set; }
        public string District { get; set; }
        public string StatusId { get; set; }
        public string CreationDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public string ModificationDate { get; set; }
        public string CallNo { get; set; }
        public string WhatsappNo { get; set; }

    }
    public class LoginModel
    {
        public bool Result { get; set; }
        public LoginObject Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public string RelativeUrl { get; set; }

    }
}
