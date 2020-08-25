
namespace CRM.Model.AdminModel
{
    public class TransferLeadModel
    {
        public bool Result { get; set; }
        public TransferLeadData Object { get; set; }
        public string Message { get; set; }
        public bool ChangeLocation { get; set; }
        public object RelativeUrl { get; set; }
        public int TotalCount { get; set; }
    }

    public class TransferLeadData
    {
        public int TotalLead { get; set; }
        public int TotalTransferdLead { get; set; }
        public int TotalUnTransferdLead { get; set; }
    }
}