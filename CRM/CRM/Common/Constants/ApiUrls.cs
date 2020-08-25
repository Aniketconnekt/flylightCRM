namespace CRM.Common.Constants
{
    public class ApiUrls
    {
        public const string Baseurl = "http://flylight.connekt.in///api/Service/";

        // Common APIs 
        public const string LoginUrl = Baseurl + "Login?MobileNumber={0}&Password={1}";
        public const string CheckMobileNoUrl = Baseurl + "CheckMobileNoExist?mobileno={0}";
        public const string ForgotPasswordUrl = Baseurl + "ForGotPassword?MobileNumber={0}&NewPassword={1}";
        public const string GetStateListUrl = Baseurl + "GetStateList?CountryId={0}";
        public const string SearchCampaignListUrl = Baseurl + "SearchCampaignList?userid={0}&Searchval={1}";
        public const string SendotponmobileUrl = Baseurl + "Sendotponmobile?message={0}&mobileno={1}";

        // Super Admin APIs
        public const string GetDashboardDataForAdminUrl = Baseurl + "GetDashboardDataForAdmin";
        public const string SaveAdminUserUrl = Baseurl + "SaveAdminUser";
        public const string UpdateAdminUserUrl = Baseurl + "UpdateAdminUser";
        public const string GetAdminUserListUrl = Baseurl + "GetAdminUserList";
        public const string DeleteAdminUserUrl = Baseurl + "DeleteAdminUser?userid={0}";
        public const string GetAdminMoredetailUrl = Baseurl + "GetAdminMoredetail?Adminid={0}";
        public const string GetcampdetailbyuseridUrl = Baseurl + "Getcampdetailbyuserid?userid={0}";
        public const string GetUserListByAdminIdUrl = Baseurl + "GetUserListByAdminId?AdminId={0}&lastrecordsid={1}"; 
        public const string SearchAdminUserListUrl = Baseurl + "SearchAdminUserList?SearchValue={0}";

        //Admin APIs
        public const string SaveUserUrl = Baseurl + "SaveUser";
        public const string UpdateUserUrl = Baseurl + "UpdateUser";
        public const string DeleteUserUrl = Baseurl + "DeleteUser?userids={0}";
        public const string GetDashboardDataForAdminUserUrl = Baseurl + "GetDashboardDataForAdminUser?AdminId={0}";
        public const string AddNewCampaignUrl = Baseurl + "AddNewCampaign";
        public const string DeleteCampaignUrl = Baseurl + "DeleteCampaign?Campid={0}";
        public const string AddUpdateLeadUrl = Baseurl + "AddUpdateLead";
        public const string SearchLeadsbycampidUrl = Baseurl + "SearchLeadsbycampid?AdminId={0}&Campid={1}";
        public const string AllotLeadsToUserUrl = Baseurl + "AllotLeadsToUser?Leadsid={0}&Userid={1}";
        public const string GetLeadsListForAdminUrl = Baseurl + "GetLeadsListForAdmin?AdminId={0}&lastrecordsid={1}";  
        public const string DeleteLeadsUrl = Baseurl + "DeleteLeads?Leads={0}";
        public const string GetUserPerformanceUrl = Baseurl + "GetUserPerformance?Userid={0}&Campid={1}&DescriptionId={2}";
        public const string GetLeadsCalledListUrl = Baseurl + "GetLeadsCalledList?AdminId={0}&Fromdate={1}&Todate={2}";
        public const string GetLeadsCalledListbyuseridUrl = Baseurl + "GetLeadsCalledListbyuserid?Userid={0}&lastrecordsid={1}&Fromdate={2}&Todate={3}"; 
        public const string GetLeadsPendingListbyuseridUrl = Baseurl + "GetLeadsPendingListbyuserid?Userid={0}&lastrecordsid={1}&Fromdate={2}&Todate={3}";
        public const string LeadBulkUploadUrl = Baseurl + "LeadBulkUpload";
        public const string TransferLeadUrl = Baseurl + "TransferLead?LeadId={0}&AssignUserid={1}";
        public const string SearchUsersUrl = Baseurl + "SearchUsers?AdminId={0}&SearchValue={1}";
        public const string SearchLeadsForAdminUrl = Baseurl + "SearchLeadsForAdmin?AdminId={0}&Searchval={1}";
        public const string GetstatusListUrl = Baseurl + "GetstatusList?UserId={0}";
        public const string AddUpdateStatusUrl = Baseurl + "AddUpdateStatus";
        public const string DeleteStatusUrl = Baseurl + "DeleteStatus?Statusid={0}";
        public const string GetLeadsDetailListForAdminUrl = Baseurl + "GetLeadsDetailListForAdmin?AdminId={0}&Campid={1}&lastrecordsid={2}";  
        public const string SearchLeadsDetailForAdminUrl = Baseurl + "SearchLeadsDetailForAdmin?AdminId={0}&Campid={1}&Searchval={2}";
        public const string GetFollowupListForAdminUrl = Baseurl + "GetFollowupListForAdmin?lastrecordsid={0}&Fromdate={1}&Todate={2}&Userid={3}"; 
        public const string TransferAllLeadToAnotherUserUrl = Baseurl + "TransferAllLeadToAnotherUser?AdminId={0}&FromUserId={1}&ToUserId={2}";
        public const string GetLeadsListForAdminbyStatusIdUrl = Baseurl + "GetLeadsListForAdminbyStatusId?UserId={0}&StatusId={1}&lastrecordsid={2}";
        public const string SendMessageToLeadsUrl = Baseurl + "SendMessageToLeads?message={0}&Campid={1}";

        //User APIs
        public const string GetDashboardDataUrl = Baseurl + "GetDashboardData?Userid={0}";
        public const string GetLeadsUrl = Baseurl + "GetLeads?userid={0}&lastrecordsid={1}"; 
        public const string AddUpdateLeadUserUrl = Baseurl + "AddUpdateLeadUser";
        public const string UpdateAlotedLeadByUserUrl = Baseurl + "UpdateAlotedLeadByUser";
        public const string GetCampaignListUrl = Baseurl + "GetCampaignList?Userid={0}";
        public const string GetLadAction = Baseurl + "GetLadActionList?CreatedbyId={0}";
        public const string GetUserByCampIdUrl = Baseurl + "GetUserByCampId?Campid={0}";
        public const string GetFollowupListForUserUrl = Baseurl + "GetFollowupListForUser?Campid={0}&lastrecordsid={1}&Fromdate={2}&Todate={3}&Userid={4}"; 
        public const string GetLeadsbycampidUrl = Baseurl + "GetLeadsbycampid?Userid={0}&Campid={1}&lastrecordsid={2}"; 
        public const string SaveLeadCreditsUrl = Baseurl + "SaveLeadCredits";
        public const string SendReminderUrl = Baseurl + "SendReminder?Leadid={0}&Meaasge={1}";
        public const string GetLeadsListFromPerformanceUrl = Baseurl + "GetLeadsListFromPerformance?userid={0}&lastrecordsid={1}&Status={2}&CampId={3}"; 
        public const string GetAllotedLeadsUrl = Baseurl + "GetAllotedLeads?userid={0}&lastrecordsid={1}&CampId={2}"; 
        public const string UpdateAllotedLeadUrl = Baseurl + "UpdateAllotedLead";
        public const string SearchleadslistUrl = Baseurl + "Searchleadslist?status={0}&mobnumber={1}&name={2}&Campid={3}&userid={4}&lastrecordsid={5}"; 
        public const string GetLeadsForUsersUrl = Baseurl + "GetLeadsForUsers?userid={0}&lastrecordsid={1}"; 
    }
}
