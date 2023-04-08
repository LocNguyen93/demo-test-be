using Dapper;
using System.Data;
using Ext.Shared.Models;
using System.Collections.Generic;
using static Dapper.SqlMapper;
using System.Reflection;
using System;
using System.Linq;

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

        public static DynamicParameters AddParamTvp(this DynamicParameters parameters, string paramName, ICustomQueryParameter customQueryParameter)
        {
            parameters.Add(paramName, customQueryParameter);
            return parameters;
        }

        public static DynamicParameters AddParamTvpString(this DynamicParameters parameters, string paramName, List<string> values)
            => listToTvp(parameters, values, paramName, "tvp_String");

        public static DynamicParameters AddParamTvpInt(this DynamicParameters parameters, string paramName, List<int> values)
            => listToTvp(parameters, values, paramName, "tvp_Integer");

        public static DynamicParameters AddParamTvpBigInt(this DynamicParameters parameters, string paramName, List<long> values)
            => listToTvp(parameters, values, paramName, "tvp_BigInt");

        public static DynamicParameters AddPagingParams(this DynamicParameters parameters, PagingParamModel paging)
        {
            parameters.Add("@PageNum", paging.Page);
            parameters.Add("@PageSize", paging.Size);

            return parameters;
        }

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

            return new Result<T>(errCode, errMsg, parameters.Get<T>(dataParamName) ?? default);
        }

        private static DynamicParameters listToTvp<T>(this DynamicParameters parameters, List<T> values, string paramName, string tvpName)
        {
            var dt = new DataTable();
            dt.Columns.Add("Value", typeof(T));

            foreach (var val in values)
            {
                var dr = dt.NewRow();
                dr["Value"] = val;

                dt.Rows.Add(dr);
            }

            parameters.Add(paramName, dt.AsTableValuedParameter(tvpName));

            return parameters;
        }

        public static DataTable AddDataFromList<T>(this DataTable dt, List<T> list)
        {
            foreach (PropertyInfo prop in typeof(T).GetProperties())
            {
                var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                dt.Columns.Add(prop.Name, type);
            }

            if (list.Count < 1) return dt;

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var dr = dt.NewRow();
                foreach (PropertyInfo prop in item.GetType().GetProperties())
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;

                    if (type == typeof(string))
                        dr[prop.Name] = prop.GetValue(item, null).ToString();
                    else if (type == typeof(int))
                    {
                        var isParsed = int.TryParse(prop.GetValue(item, null).ToString(), out int res);
                        dr[prop.Name] = isParsed ? res : 0;
                    }
                    else if (type == typeof(long))
                    {
                        var isParsed = long.TryParse(prop.GetValue(item, null).ToString(), out long res);
                        dr[prop.Name] = isParsed ? res : 0;
                    }
                    else if (type == typeof(bool))
                    {
                        var isParsed = bool.TryParse(prop.GetValue(item, null).ToString(), out bool res);
                        dr[prop.Name] = isParsed ? res : false;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}
