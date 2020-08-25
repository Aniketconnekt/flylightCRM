using CRM.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.SuperAdmin.Model
{
    public class UsersModel
    {
        public bool Result { get; set; }
        public UsersData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
        public int TotalCount { get; set; }
    }

    public class UsersData : BaseModel
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
        public string CampaignId { get; set; }
        public string Address { get; set; }
        public string CityId { get; set; }
        public string StateId { get; set; }
        public string District { get; set; }
        public string StatusId { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedById { get; set; }
        public string ModifiedById { get; set; }
        public DateTime ModificationDate { get; set; }
        public string UserType { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }

        private bool _isSelectAllChecked = false;
        public bool IsSelectAllChecked
        {
            get => _isSelectAllChecked;
            set { _isSelectAllChecked = value; OnPropertyChanged(); }
        }
    }
}
