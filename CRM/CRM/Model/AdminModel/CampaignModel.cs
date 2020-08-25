namespace CRM.Model.AdminModel
{
    public class CampaignModel
    {
        public bool Result { get; set; }
        public CampaignData[] List { get; set; }
        public int Count { get; set; }
        public object IList { get; set; }
        public string Message { get; set; }
    }

    public class CampaignData : BaseModel
    {
        public int Id { get; set; }
        public string Campaign { get; set; }
        public string Description { get; set; }
        public int TotalLeads { get; set; }
        public int VisitedLeads { get; set; }
        public int PendingLeads { get; set; }

        private bool _isSelectAllChecked = false;
        public bool IsSelectAllChecked
        {
            get => _isSelectAllChecked;
            set { _isSelectAllChecked = value; OnPropertyChanged(); }
        }
    }

}
