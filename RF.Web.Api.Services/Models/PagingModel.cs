namespace RF.Web.Api.Services.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PagingModel
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string OrderBy { get; set; }

        public PagingModel()
        {
            Page = 1;
            Size = 10;
        }

        public static PagingModel ParsePagingParams(int page, int size, string orderBy = "")
        {
            if (page < 0)
                page = 1;
            if (size < 0 || size > 100)
                size = 10;

            var model = new PagingModel
            {
                Page = page,
                Size = size,
                OrderBy = ParseSortString(orderBy)
            };

            return model;
        }

        private static string ParseSortString(string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return "";

            var orderParams = orderByQueryString.Trim().Split(',');
            List<string> propertyInfos = new List<string> { "name", "email" };

            var param = orderParams[0];
            if (string.IsNullOrWhiteSpace(param))
                return "";

            var propertyFromQueryName = param.Split(' ')[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Equals(propertyFromQueryName.Trim(), StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                return "";

            var sortingOrder = param.EndsWith(" desc", StringComparison.InvariantCultureIgnoreCase) ? "desc" : "";

            var orderQuery = $"{objectProperty} {sortingOrder}";

            if (string.IsNullOrWhiteSpace(orderQuery))
                return "";

            return orderQuery;
        }
    }
}
