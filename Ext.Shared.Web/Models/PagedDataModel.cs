namespace Ext.Shared.Web.Models
{
    using Newtonsoft.Json;
    using System.Collections;

    public class PagedDataModel
    {
        [JsonProperty(PropertyName = "t")]
        public int TotalCount { get; set; }

        [JsonProperty(PropertyName = "p")]
        public int Page { get; set; }

        [JsonProperty(PropertyName = "s")]
        public int Size { get; set; }

        [JsonProperty(PropertyName = "c", NullValueHandling = NullValueHandling.Ignore)]
        public IEnumerable Content { get; set; } = null;

        private PagedDataModel() { }
        public PagedDataModel(IEnumerable content, int page, int size, int total)
        {
            Content = content;
            Page = page;
            Size = size;
            TotalCount = total;
        }
    }

    public static class PagedDataModelExtensions
    {
        public static PagedDataModel ToPagedDataModel(this IEnumerable data, int page, int size, int total)
        {
            return new PagedDataModel(data, page, size, total);
        }
    }
}
