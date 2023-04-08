namespace Ext.Shared.DataAccess
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    public class SqlHelper
    {
        public static Task<List<T>> QueryAsync<T>(string connectionStr, string query, Func<IDataRecord, T> mappingFunc, params SqlParameter[] parameters)
        {
            return ExecuteCommandAsync(connectionStr, query, async (cmd) =>
            {
                using (var r = await cmd.ExecuteReaderAsync())
                    return ExtractData(r, mappingFunc);
            }, parameters);
        }

        public static Task<T> QueryFirstAsync<T>(string connectionStr, string query, Func<IDataRecord, T> mappingFunc, params SqlParameter[] parameters)
        {
            return ExecuteCommandAsync(connectionStr, query, async (cmd) =>
            {
                using (var r = await cmd.ExecuteReaderAsync())
                    return GetFirstRow(r, mappingFunc);
            }, parameters);
        }

        public static Task<object> ExecuteScalarAsync(string connectionStr, string query, params SqlParameter[] parameters)
        {
            return ExecuteCommandAsync(connectionStr, query, async (cmd) =>
            {
                return await cmd.ExecuteScalarAsync();
            }, parameters);
        }

        public static Task<int> ExecuteNonQueryAsync(string connectionStr, string query, params SqlParameter[] parameters)
        {
            return ExecuteCommandAsync(connectionStr, query, async (cmd) =>
            {
                return await cmd.ExecuteNonQueryAsync();
            }, parameters);
        }

        public static List<T> QueryStoredProcedure<T>(string connectionStr, string spName, Func<IDataRecord, T> mappingFunc, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedure(connectionStr, spName, cmd =>
            {
                using (var r = cmd.ExecuteReader())
                    return ExtractData(r, mappingFunc);
            }, parameters);
        }

        public static T QueryFirstStoredProcedure<T>(string connectionStr, string spName, Func<IDataRecord, T> mappingFunc, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedure(connectionStr, spName, cmd =>
            {
                using (var r = cmd.ExecuteReader())
                    return GetFirstRow(r, mappingFunc);
            }, parameters);
        }

        public static List<T> QueryStoredProcedure<T>(string connectionStr, string spName, Func<IDataRecord, T> mappingFunc, out Result executionResult, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedure(connectionStr, spName, cmd =>
            {
                using (var r = cmd.ExecuteReader())
                    return ExtractData(r, mappingFunc);
            }, out executionResult, parameters);
        }

        public static T QueryFirstStoredProcedure<T>(string connectionStr, string spName, Func<IDataRecord, T> mappingFunc, out Result executionResult, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedure(connectionStr, spName, cmd =>
            {
                using (var r = cmd.ExecuteReader())
                    return GetFirstRow(r, mappingFunc);
            }, out executionResult, parameters);
        }

        public static Task<List<T>> QueryStoredProcedureAsync<T>(string connectionStr, string spName, Func<IDataRecord, T> mappingFunc, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedureAsync(connectionStr, spName, async (cmd) =>
            {
                using (var r = await cmd.ExecuteReaderAsync())
                    return ExtractData(r, mappingFunc);
            }, parameters);
        }

        public static Task<T> QueryFirstStoredProcedureAsync<T>(string connectionStr, string spName, Func<IDataRecord, T> mappingFunc, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedureAsync(connectionStr, spName, async (cmd) =>
            {
                using (var r = await cmd.ExecuteReaderAsync())
                    return GetFirstRow(r, mappingFunc);
            }, parameters);
        }

        public static int ExecuteStoredProcedure(string connectionStr, string spName, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedure(connectionStr, spName, cmd => cmd.ExecuteNonQuery(), parameters);
        }

        public static int ExecuteStoredProcedure(string connectionStr, string spName, out Result executionResult, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedure(connectionStr, spName, cmd => cmd.ExecuteNonQuery(), out executionResult, parameters);
        }

        public static Task<int> ExecuteStoredProcedureAsync(string connectionStr, string spName, params SqlParameter[] parameters)
        {
            return ExecuteStoredProcedureAsync(connectionStr, spName, async (cmd) =>
            {
                return await cmd.ExecuteNonQueryAsync();
            }, parameters);
        }

        private static async Task<T> ExecuteCommandAsync<T>(string connectionStr, string query, Func<SqlCommand, Task<T>> executeCommandFunc, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = query;
                    if (parameters != null && parameters.Any())
                        cmd.Parameters.AddRange(parameters);
                    cmd.Connection.Open();
                    return await executeCommandFunc(cmd);
                }
            }
        }

        private static async Task<T> ExecuteStoredProcedureAsync<T>(string connectionStr, string spName, Func<SqlCommand, Task<T>> executeStoredProcedureFunc, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = spName;
                    if (parameters != null && parameters.Any())
                    {
                        cmd.Parameters.AddRange(parameters.Where(x => x.SqlDbType != SqlDbType.Structured).ToArray());

                        var structuredParams = parameters.Where(x => x.SqlDbType == SqlDbType.Structured);
                        if (structuredParams.Any())
                        {
                            cmd.Parameters.AddRange(structuredParams.Select(x =>
                                new SqlParameter(x.ParameterName, SqlDbType.Structured)
                                {
                                    Value = x.Value is IEnumerable ? CreateDataTable(x.Value as IEnumerable) : x.Value
                                }).ToArray());
                        }
                    }
                    cmd.Connection.Open();
                    return await executeStoredProcedureFunc(cmd);
                }
            }
        }

        private static T ExecuteStoredProcedure<T>(string connectionStr, string spName, Func<SqlCommand, T> executeStoredProcedureFunc, params SqlParameter[] parameters)
        {
            using (var conn = new SqlConnection(connectionStr))
            {
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = spName;
                    if (parameters != null && parameters.Any())
                    {
                        cmd.Parameters.AddRange(parameters.Where(x => x.SqlDbType != SqlDbType.Structured).ToArray());

                        var structuredParams = parameters.Where(x => x.SqlDbType == SqlDbType.Structured);
                        if (structuredParams.Any())
                        {
                            cmd.Parameters.AddRange(structuredParams.Select(x =>
                                new SqlParameter(x.ParameterName, SqlDbType.Structured)
                                {
                                    Value = x.Value is IEnumerable ? CreateDataTable(x.Value as IEnumerable) : x.Value
                                }).ToArray());
                        }
                    }
                    cmd.Connection.Open();
                    return executeStoredProcedureFunc(cmd);
                }
            }
        }

        private static T ExecuteStoredProcedure<T>(string connectionStr, string spName, Func<SqlCommand, T> executeStoredProcedureFunc, out Result executionResult, params SqlParameter[] parameters)
        {
            var errorCodeParam = new SqlParameter("O_ErrorCode", SqlDbType.VarChar, 50)
            {
                Direction = ParameterDirection.Output
            };
            var errorMessageParam = new SqlParameter("O_ErrorMessage", SqlDbType.VarChar, 8000)
            {
                Direction = ParameterDirection.Output
            };

            if (parameters == null)
                parameters = new SqlParameter[0];
            var newParams = parameters.ToList();
            newParams.Add(errorCodeParam);
            newParams.Add(errorMessageParam);

            var result = ExecuteStoredProcedure(connectionStr, spName, executeStoredProcedureFunc, newParams.ToArray());
            executionResult = new Result(errorCodeParam, errorMessageParam);
            return result;
        }
           
        private static List<T> ExtractData<T>(SqlDataReader reader, Func<IDataRecord, T> mappingFunc)
        {
            return ((DbDataReader)reader).Cast<IDataRecord>().Select(mappingFunc).ToList();
        }

        private static T GetFirstRow<T>(SqlDataReader reader, Func<IDataRecord, T> mappingFunc)
        {
            var firstRow = ((DbDataReader)reader).Cast<IDataRecord>().FirstOrDefault();
            if (firstRow == null)
                return default;
            return mappingFunc(firstRow);
        }

        private static DataTable CreateDataTable(IEnumerable list)
        {
            Type type = list.GetType().GetGenericArguments()[0];
            var properties = type.GetProperties();

            DataTable dataTable = new DataTable();
            foreach (PropertyInfo info in properties)
            {
                dataTable.Columns.Add(new DataColumn(info.Name, Nullable.GetUnderlyingType(info.PropertyType) ?? info.PropertyType));
            }

            foreach (object entity in list)
            {
                object[] values = new object[properties.Length];
                for (int i = 0; i < properties.Length; i++)
                {
                    values[i] = properties[i].GetValue(entity);
                }

                dataTable.Rows.Add(values);
            }

            return dataTable;
        }
    }
}
