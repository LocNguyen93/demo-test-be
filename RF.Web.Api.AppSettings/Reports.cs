namespace RF.Web.Api.AppSettings
{
    public class Reports
    {
        public MonthlyKpi MonthlyKpi { get; set; }
    }

    public class MonthlyKpi
    {
        public string OutputFilePath { get; set; }
        public string S3Key { get; set; }
        public string SheetName { get; set; }
    }
}
