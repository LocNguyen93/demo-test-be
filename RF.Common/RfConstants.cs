namespace RF.Common
{
    public static class RfConstants
    {
        public const int WelcomeSurveyId = 1;

        public static class NewsType
        {
            public const string Media = "NEWS";
            public const string Leadership = "LEADERSHIP";
            public const string Video = "VIDEO";
            public const string Social = "SOCIAL";
        }

        public static class FileUploadNames
        {
            public const string Photo = "Photo";
            public const string Background = "Background";
            public const string IntroductionPhoto = "IntroductionPhoto";
            public const string ContentFile = "ContentFile";
            public const string File = "File";
            public const string SignedPhoto = "SignedPhoto";
            public const string DataroomItem = "DataroomItem";
        }

        public static class UserTypes
        {
            public const string Rokker = "Rokker";
            public const string Founder = "Founder";
            public const string Investor = "Investor";
            public const string IDC = "IDC";
        }

        public static class InvestorTypes
        {
            public const string Current = "Current";
            public const string Potential = "Potential";
            public const string Delegate = "Delegate";
        }

        public static class ClaimTypes
        {
            public const string UserType = "utype";
            public const string PortfolioId = "pid";
        }

        public static class Roles
        {
            public const string IDCUser = "IDCUser";
        }

        public static class Permissions
        {
            public const string Dashboard_Read = "Dashboard_Read";
            
            public const string Surveys_Read = "Surveys_Read";
            public const string Surveys_Create = "Surveys_Create";
            public const string Surveys_Update = "Surveys_Update";
            public const string Surveys_Delete = "Surveys_Delete";
            public const string Surveys_Review = "Surveys_Review";

            public const string Partners_KPI_Report = "Partners_KPI_Report";
            public const string Partners_KPI_Manage = "Partners_KPI_Manage";
            public const string Partners_Read = "Partners_Read";
            public const string Partners_Update = "Partners_Update";

            public const string Members_Read = "Members_Read";
            public const string Members_Create = "Members_Create";
            public const string Members_Update = "Members_Update";
            public const string Members_Delete = "Members_Delete";

            public const string Funds_Read = "Funds_Read";
            public const string Funds_Create = "Funds_Create";
            public const string Funds_Update = "Funds_Update";
            public const string Funds_Delete = "Funds_Delete";

            public const string Portfolios_Read = "Portfolios_Read";
            public const string Portfolios_Create = "Portfolios_Create";
            public const string Portfolios_Update = "Portfolios_Update";
            public const string Portfolios_Delete = "Portfolios_Delete";

            public const string News_Read = "News_Read";
            public const string News_Create = "News_Create";
            public const string News_Update = "News_Update";
            public const string News_Delete = "News_Delete";

            public const string Users_Read = "Users_Read";
            public const string Users_Create = "Users_Create";
            public const string Users_Update = "Users_Update";
            public const string Users_Delete = "Users_Delete";
            public const string Users_AssignRoles = "Users_AssignRoles";

            public const string Founders_Read = "Founders_Read";
            public const string Founders_Update = "Founders_Update";
            public const string Founders_Create = "Founders_Create";

            public const string Guest_Registration_Report = "Guest_Registration_Report";

            public const string Investors_Read = "Investors_Read";
            public const string Investors_Create = "Investors_Create";
            public const string Investors_Update = "Investors_Update";
            public const string Investors_Delete = "Investors_Delete";

            public const string Config_Investors_Read = "Config_Investors_Read";
            public const string Config_Investors_Create = "Config_Investors_Create";
            public const string Config_Investors_Update = "Config_Investors_Update";
            public const string Config_Investors_Delete = "Config_Investors_Delete";

            public const string InvestorRoles_Read = "InvestorRoles_Read";
            public const string InvestorRoles_Create = "InvestorRoles_Create";
            public const string InvestorRoles_Update = "InvestorRoles_Update";

            public const string Config_InvestorRoles_Read = "Config_InvestorRoles_Read";
            public const string Config_InvestorRoles_Create = "Config_InvestorRoles_Create";
            public const string Config_InvestorRoles_Update = "Config_InvestorRoles_Update";

            public const string Roles_Read = "Roles_Read";
            public const string Roles_Create = "Roles_Create";
            public const string Roles_Update = "Roles_Update";
            public const string Roles_Delete = "Roles_Delete";
            public const string Roles_AssignPermissions = "Roles_AssignPermissions";

            public const string MessageTemplates_Read = "MessageTemplates_Read";
            public const string MessageTemplates_Create = "MessageTemplates_Create";
            public const string MessageTemplates_Update = "MessageTemplates_Update";
            public const string MessageTemplates_Delete = "MessageTemplates_Delete";

            public const string Dataroom_Read = "Dataroom_Read";
            public const string Report_Dataroom_InvestorFocus = "Report_Dataroom_InvestorFocus";
            public const string Report_Dataroom_InvestorEngagement = "Report_Dataroom_InvestorEngagement";

            public const string SystemParam_InstagramAccessToken = "SystemParam_InstagramAccessToken";

            public const string IDC_Partners_KPI_Report = "IDC_Partners_KPI_Report";
            public const string IDC_Partners_KPI_Manage = "IDC_Partners_KPI_Manage";
            public const string IDC_Partners_Read = "IDC_Partners_Read";
            public const string IDC_Partners_Update = "IDC_Partners_Update";

            public const string LpMedia_Read = "LpMedia_Read";
            public const string LpMedia_Create= "LpMedia_Create";
            public const string LpMedia_Update = "LpMedia_Update";
            public const string LpMedia_Delete = "LpMedia_Delete";

            public const string EmailQueue_Read = "EmailQueue_Read";
            
            public const string Dataroom_Permanently_Delete = "Dataroom_Permanently_Delete";
        }

        public static class InvestorPermissions
        {
            public const string View_Overview = "View_Overview";
            public const string View_Newsletter = "View_Newsletter";
            public const string View_CornerstonePortal = "View_CornerstonePortal";
            public const string View_InvestmentOppotunities = "View_InvestmentOppotunities";
            public const string View_Replay = "View_Replay";
        }

        public static class SurveyStatuses
        {
            public const string NotStarted = "NOT_STARTED";
            public const string Completed = "COMPLETED";
            public const string InProgress = "IN_PROGRESS";
            public const string Reviewing = "REVIEWING";
            public const string Approved = "APPROVED";
            public const string Rejected = "REJECTED";
        }

        public static class SystemParamNames
        {
            public const string InstagramAccessToken = "INSTAGRAM_ACCESS_TOKEN";
            public const string CoInvestmentOpDefaultEmail = "CO_INVESTMENT_OP_DEFAULT_EMAIL";
        }

        public static class OneTimeAccessTokenTypes
        {
            public const string S3Download = "S3Download";
        }

        public static class KpiChartDefIds
        {
            public const int EmptyBlock = -1;
            public const int Cash = 4;
            public const int BookingsTarget = 7;
            public const int SalesTarget = 8;
            public const int MRRTarget = 9;
        }

        public static class KpiChartTypes
        {
            public const string Empty = "EMPTY";
            public const string Cash = "CASH";
            public const string Target = "TARGET";
            public const string Ownership = "OWNERSHIP";
        }

        public static class DataRoomFileTypes
        {
            public const string Folder = "Folder";
            public const string File = "File";
            public const string Bookmark = "Bookmark";
        }

        public static class LPMediaTypes
        {
            public const string INVESTOR_CALL_REPLAY = "INVESTOR_CALL_REPLAY";
            public const string CO_INVESTMENT_OP = "CO_INVESTMENT_OP";
            public const string OVERVIEW = "OVERVIEW";
            public const string NEWLETTER = "NEWLETTER";
            public const string US_IN_THE_NEWS = "US_IN_THE_NEWS";
            public const string WHAT_NEWS = "WHAT_NEWS";
            public const string COMPANIES_IN_THE_NEWS = "COMPANIES_IN_THE_NEWS";
            public const string EVENT = "EVENT";
        }

        public static class MediaCategories
        {
            public const string PHOTO = "PHOTO";
            public const string VIDEO = "VIDEO";
            public const string PDF = "PDF";
            public const string DOC = "DOC";
        }

        public enum CommunicationStatus
        {
            PENDING,
            DONE
        }

        public enum CommunicationType
        {
            HELP_TICKET,
            CALL_TO_ACTION_TICKET
        }

        public enum MessageTemplatesEmailType
        {
            INVITATION_EMAIL,
            DATAROOM_EMAIL
        }
    }
}
