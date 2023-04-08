namespace RF.Common
{
    public static class AuditLogConstants
    {
        public static class Actions
        {
            public const string Create = "Create";
            public const string Update = "Update";
            public const string SortOrder = "SortOrder";
            public const string Delete = "Delete";
            public const string Login = "Login";
            public const string MoveTo = "MoveTo";
            public const string CopyTo = "CopyTo";
            public const string Trash = "Trash";
            public const string Restore = "Restore";
            public const string AddVersion = "AddVersion";
            public const string AddWatermark = "AddWatermark";
            public const string SetCurrentVersion = "SetCurrentVersion";
            public const string ForgotPassword = "ForgotPassword";
            public const string ResetPassword = "ResetPassword";
            public const string UpdateProfile = "UpdateProfile";
            public const string Register = "Register";
            public const string ResendConfirmEmail = "ResendConfirmEmail";
            public const string VerifyAccount = "VerifyAccount";
            public const string ChangePassword = "ChangePassword";
            public const string UpdateStatus = "UpdateStatus";
            public const string InviteInvestor = "InviteInvestor";
            public const string ReInvite = "ReInvite";
            public const string ConfirmEmail = "ConfirmEmail";
            public const string Unlock = "Unlock";
            public const string LoginFailed_WrongUserType = "LoginFailed_WrongUserType";
            public const string LoginFailed_LockedOut = "LoginFailed_LockedOut";
            public const string LoginFailed_WrongPassword = "LoginFailed_WrongPassword";
            public const string LoginFailed_InactiveAccount = "LoginFailed_InactiveAccount";
            public const string LoginFailed_UserLocked = "LoginFailed_UserLocked";
            public const string LoginFailed_UserExpired = "LoginFailed_UserExpired";
            public const string LoginSuccess = "LoginSuccess";
            public const string InvestorSignNDA = "InvestorSignNDA";
            public const string ResetLockout = "ResetLockout";
            public const string AddMembers = "AddMembers";
        }

        public static class Categories
        {
            public const string Users = "Users";
            public const string Roles = "Roles";
            public const string UserRoles = "UserRoles";

            public const string TeamMembers = "TeamMembers";
            public const string Funds = "Funds";
            public const string FundMembers = "FundMembers";
            public const string Portfolios = "Portfolios";
            public const string News = "News";

            public const string Surveys = "Surveys";
            public const string SurveySections = "SurveySections";
            public const string SurveyQuestions = "SurveyQuestions";

            public const string UserSurveyQuestions = "UserSurveyQuestions";

            public const string DataRoomItems = "DataRoomItems";
            public const string DataRoomItemVersions = "DataRoomItemVersions";

            public const string LPMedia = "LPMedia";
            public const string SystemParams = "SystemParams";

            public const string KpiData_Ownership = "KpiData_Ownership";
            public const string KpiData_Goal = "KpiData_Goal";
            public const string KpiData = "KpiData";

            public const string PartnerDataView = "PartnerDataView";
            public const string MessageTemplate = "MessageTemplate";
        }
    }
}
