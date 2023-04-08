namespace RF.Web.Api.AppSettings
{
    public class IronPdfWatermark
    {
        public string LicenseKey { get; set; }
        public bool IsImg { get; set; }
        public Watermark Watermark { get; set; }
        public WatermarkImg WatermarkImg { get; set; }     
    }

    public class Watermark
    {
        public string FontColor { get; set; }
        public string FontSize { get; set; }
        public string FontWeight { get; set; }
        public string Text { get; set; }
        public int Location { get; set; }
        public int Opacity { get; set; }
        public int Rotation { get; set; }
        public string Hyperlink { get; set; }
    }

    public class WatermarkImg
    {
        public string Url { get; set; }
        public string Width { get; set; }
    }
}
