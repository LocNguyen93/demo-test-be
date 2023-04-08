using System;
using System.Collections.Generic;
using System.Linq;

namespace Ext.Shared.DataAccess
{
    public abstract class BaseService
    {
        protected Result Succeed()
        {
            return new Result();
        }

        protected Result<T> Succeed<T>(T data)
        {
            return new Result<T>(data);
        }

        protected Result Error(string errorCode, string errorMsg = "")
        {
            return new Result(errorCode, errorMsg);
        }

        protected Result<T> Error<T>(string errorCode, string errorMsg = "")
        {
            return new Result<T>(errorCode, errorMsg, default(T));
        }

        public string ParseSortString(string orderByQueryString)
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
