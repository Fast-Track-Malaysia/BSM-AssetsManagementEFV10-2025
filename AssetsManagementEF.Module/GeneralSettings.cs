using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetsManagementEF.Module
{
    public class MyMsg
    {
        public int id { get; set; }
        public string msg { get; set; }
    }
    public static class GeneralSettings
    {
        public static SAPbobsCOM.Company oCompany;

        public const string DeviationStatusCancel = "Cancel";
        public const string DeviationStatusNew = "New";
        public const string DeviationStatusDraft = "Draft";
        public const string DeviationStatusUnderReview = "Under Review";
        public const string DeviationStatusSubmitAck = "Submit Acknowledge";
        public const string DeviationStatusPendingApproval = "Pending Approval";
        public const string DeviationStatusApproved = "Approved";
        public const string DeviationStatusExpired = "Expired";
        public const string DeviationStatusDraftExtension = "Draft Extension";
        public const string DeviationStatusApprovedExtension = "Approved Extension";
        public const string DeviationStatusWithdrawn = "Withdrawn";
        public const string DeviationStatusOpen = "Open";
        public const string DeviationStatusClose = "Closed";

        public const string DeviationTypeOLAFD = "OLAFD";
        public const string DeviationTypeSCE = "SCE";

        public const int sceid = 4;
        public static int contractdocid = 0;
        public static DateTime pmstartdate = DateTime.Parse("2018-01-01");
        public static int AssetsLenght = 5;
        public static int DocLenght = 8;
        public static string HQCompany = "HQ";
        public static string PurchaseRequest = "PR";
        public static string defaultcode = "-";

        public static string WR = "WR";

        public static string WoPlantMaintenance = "PM";
        public static string WoBreakdown = "CM";
        public static string Deviation = "DE";
        //public static string WoSurveillancePlantMaintenance = "SPM";
        //public static string WoSurveillanceBreakdown = "SCM";

        public static string EqDown = "DOWN";
        public static string EqUp = "UP";

        public static string InitCMJobStatus = "AP";
        public static string InitPMJobStatus = "AA";
        public static string ClosureJobStatus = "TC";

        public static string WOPlanDateRole = "WOPlanDateRole";
        public static string PostPRRole = "PostPR";
        public static string CancelPRRole = "CancelPRRole";
        public static string CancelWRRole = "CancelWRRole";
        public static string CancelWORole = "CancelWORole";
        public static string ManagerRole = "Manager";
        public static string ApproverRole = "Approver";
        public static string PlannerRole = "Planner";
        public static string WPSRole = "WPS";
        public static string SupervisorRole = "Supervisor";
        public static string TechnicianRole = "Technician";
        public static string RequestorRole = "Requestor";
        public static string ContractorRole = "Contractor";
        public static string WRSupervisorRole = "WRSupervisor";
        public static string GeneratePMRole = "GeneratePM";
        public static string AcknowledgePMRole = "AcknowledgePM";
        public static string AdminRole = "Administrators";
        public static string DeviationUser = "DeviationUser";
        public static string DeviationManager = "DeviationManager";
        public static string DeviationApprover = "DeviationApprover";
        public static string DeviationReopen = "DeviationReopen";
        public static string DeviationReviewer = "DeviationReviewer";

        public static bool EmailSend;
        public static string EmailHost = "";
        public static string EmailPort = "";
        public static string Email = "";
        public static string EmailPassword = "";
        public static string EmailName = "";
        public static bool EmailSSL;
        public static bool EmailUseDefaultCredential;

        public static bool B1Post;
        public static string B1UserName = "";
        public static string B1Password = "";
        public static string B1Server = "";
        public static string B1CompanyDB = "";
        public static string B1License = "";
        public static string B1Sld = "";
        public static string B1Language = "";
        public static string B1DbServerType = "";
        public static string B1DbUserName = "";
        public static string B1DbPassword = "";
        public static int B1PRseries = 0;
        public static string B1AttachmentPath = "";

        public static string B1PRCol = "";
        public static string B1PRNoCol = "";
        public static string B1WONoCol = "";
        public static string B1PRLineIDCol = "";

        public static string B1CTCol = "";
        public static string B1CTNoCol = "";

    }
}
