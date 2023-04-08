namespace RF.Web.Api.AppSettings
{
    public class Aws
    {
        public Credential Credential { get; set; }
        public string Region { get; set; }
        public S3 S3 { get; set; }
    }

    public class Credential
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
    }

    public class S3
    {
        public string Bucket { get; set; }
        public string TeamMemberPhotoPath { get; set; }
        public string PortfolioPhotoPath { get; set; }
        public string EventFilePath { get; set; }
        public string NewsPhotoPath { get; set; }
        public string BaseUrl { get; set; }
        public string MonthlyKpiTemplateFile { get; set; }
        public NDAForm Nda { get; set; }
        public NDAForm InvestorNda { get; set; }
        public string DataRoomFilePath { get; set; }
        public string DataRoomTmpFilePath { get; set; }
        public int SessionTokenLifeTimeMinutes { get; set; }
        public long UploadPartSize { get; set; }
    }

    public class NDAForm
    {
        public string FormTemplateFile { get; set; }
        public string OutputSignedPath { get; set; }
        public FormFieldInfo[] Fields { get; set; }
        public LocationInfo[] Signatures { get; set; }
    }

    public class LocationInfo
    {
        public int PageIndex { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float W { get; set; }
        public float H { get; set; }
    }

    public class FormFieldInfo
    {
        public string FieldName { get; set; }
        public string ObjectFieldName { get; set; }
    }
}
