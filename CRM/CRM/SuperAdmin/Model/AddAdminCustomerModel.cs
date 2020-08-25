using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.SuperAdmin.Model
{
    public class AddAdminCustomerModel : BaseModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string DOB { get; set; }
        public string MobileNumber { get; set; }
        public string AlternateNo { get; set; }
        public string Company { get; set; }
        public string GSTNo { get; set; }
        public string Address { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string District { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreatedById { get; set; }
    }
}
