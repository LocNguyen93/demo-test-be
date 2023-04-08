using Dapper;
using System.Data;

namespace Ext.Shared.DataAccess.Dapper
{
    public static class DynamicParameterExtensions
    {
        public static DynamicParameters AddParam(this DynamicParameters parameters, string paramName, object value = null, DbType? dbType = null,
            ParameterDirection? direction = null, int? size = null, byte? precision = null, byte? scale = null)
        {
            if (value != null && value.GetType().IsEnum)
                parameters.Add(paramName, value.ToString(), dbType, direction, size, precision, scale);
            else
                parameters.Add(paramName, value, dbType, direction, size, precision, scale);

            return parameters;
        }

        //public static DynamicParameters AddPagingParams(this DynamicParameters parameters, PagingParamModel paging)
        //{
        //    parameters.Add("@PageNum", paging.Page);
        //    parameters.Add("@PageSize", paging.Size);

        //    return parameters;
        //}

        public static DynamicParameters AddResultParams(this DynamicParameters parameters)
        {
            parameters.Add("@O_ErrorCode", dbType: DbType.String, size: 50, direction: ParameterDirection.Output);
            parameters.Add("@O_ErrorMessage", dbType: DbType.String, size: 8000, direction: ParameterDirection.Output);

            return parameters;
        }

        public static Result ParseExecutionResult(this DynamicParameters parameters)
        {
            var errCode = parameters.Get<string>("@O_ErrorCode");
            var errMsg = parameters.Get<string>("@O_ErrorMessage");

            return new Result(errCode, errMsg);
        }

        public static Result<T> ParseExecutionResult<T>(this DynamicParameters parameters, string dataParamName)
        {
            var errCode = parameters.Get<string>("@O_ErrorCode");
            var errMsg = parameters.Get<string>("@O_ErrorMessage");
            var obj = parameters.Get<T>(dataParamName);

            return new Result<T>(errCode, errMsg, obj == null ? default : obj);
        }
    }
}
