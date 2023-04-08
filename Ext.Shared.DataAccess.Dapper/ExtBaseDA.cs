using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Dapper;
using System.Data;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;

namespace Ext.Shared.DataAccess.Dapper
{
    public abstract class ExtBaseDA
    {
        protected string ConnectionString { get; set; }

        public ExtBaseDA(string connectionStr)
        {
            ConnectionString = connectionStr;
            SqlMapper.AddTypeHandler(new JArrayTypeHandler());
            SqlMapper.AddTypeHandler(new JObjectTypeHandler());
        }

        public class JArrayTypeHandler : SqlMapper.TypeHandler<JArray>
        {
            public override JArray Parse(object value)
            {
                string json = value.ToString();
                return JArray.Parse(value.ToString());
            }

            public override void SetValue(IDbDataParameter parameter, JArray value)
            {
                parameter.Value = value.ToString();
            }
        }

        public class JObjectTypeHandler : SqlMapper.TypeHandler<JObject>
        {
            public override JObject Parse(object value)
            {
                string json = value.ToString();
                return JObject.Parse(value.ToString());
            }

            public override void SetValue(IDbDataParameter parameter, JObject value)
            {
                parameter.Value = value.ToString();
            }
        }

        protected virtual async Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using var conn = new SqlConnection(ConnectionString);
            return await conn.QueryAsync<T>(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);
        }

        protected virtual async Task<IEnumerable<T>> QueryAsync<T>(string query, Func<T, IDataRecord, T> map, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using var conn = new SqlConnection(ConnectionString);
            var reader = await conn.ExecuteReaderAsync(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);

            var list = new List<T>();
            if (!reader.HasRows) return default;

            var rawParsed = reader.GetRowParser<T>();
            while (reader.Read())
            {
                T item = rawParsed(reader);
                item = map(item, reader);
                list.Add(item);
            }
            return list;
        }

        protected virtual async Task<T> QueryFirstOrDefaultAsync<T>(string query, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using var conn = new SqlConnection(ConnectionString);
            return await conn.QueryFirstOrDefaultAsync<T>(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);
        }

        protected virtual async Task<T> QueryFirstOrDefaultAsync<T>(string query, Func<T, IDataRecord, T> map, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using var conn = new SqlConnection(ConnectionString);
            var reader = await conn.ExecuteReaderAsync(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);

            T item = default;
            if (!reader.HasRows) return item;

            var rawParsed = reader.GetRowParser<T>();
            while (reader.Read())
            {
                item = rawParsed(reader);
                item = map(item, reader);
                break;
            }
            return item;
        }

        protected virtual async Task<T> ExecuteScalarAsync<T>(string query, DynamicParameters parameters = null, IDbTransaction transaction = null)
        {
            using var conn = new SqlConnection(ConnectionString);
            return await conn.ExecuteScalarAsync<T>(query, parameters, transaction);
        }

        protected virtual async Task<int> ExecuteAsync(string query, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using var conn = new SqlConnection(ConnectionString);
            return await conn.ExecuteAsync(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);
        }

        protected virtual int Execute(string query, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using var conn = new SqlConnection(ConnectionString);
            return conn.Execute(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);
        }

        protected virtual DynamicParameters ParameterBuilder()
        {
            return new DynamicParameters();
        }
    }
}
