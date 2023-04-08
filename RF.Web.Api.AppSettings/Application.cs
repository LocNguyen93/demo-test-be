namespace RF.Web.Api.AppSettings
{
    public class Application
    {
        public string BaseUrl { get; set; }
        public string EmailConfirmationUrl { get; set; }
        public string ResetPasswordUrl { get; set; }
        public string EmailInvitationUrl { get; set; }
        public string SkipCheckToken_InvestorIds { get; set; } = "";
        public bool EnableCaptcha { get; set; }
        public string CaptchaPrivateKey { get; set; }
    }
}
