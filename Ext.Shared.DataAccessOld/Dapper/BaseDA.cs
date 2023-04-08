using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace Ext.Shared.DataAccess.Dapper
{
    public abstract class BaseDA
    {
        protected string ConnectionString { get; set; }

        public BaseDA(string connectionStr)
        {
            ConnectionString = connectionStr;
        }

        protected virtual async Task<IEnumerable<T>> QueryAsync<T>(string query, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
                return await conn.QueryAsync<T>(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);
        }

        protected virtual async Task<T> QueryFirstOrDefaultAsync<T>(string query, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
                return await conn.QueryFirstOrDefaultAsync<T>(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);
        }

        protected virtual async Task<T> ExecuteScalarAsync<T>(string query, DynamicParameters parameters = null, IDbTransaction transaction = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
                return await conn.ExecuteScalarAsync<T>(query, parameters, transaction);
        }

        protected virtual async Task<int> ExecuteAsync(string query, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
                return await conn.ExecuteAsync(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);
        }

        protected virtual int Execute(string query, DynamicParameters parameters = null, bool isStoredProc = true, IDbTransaction transaction = null)
        {
            using (var conn = new SqlConnection(ConnectionString))
                return conn.Execute(query, parameters, transaction, commandType: isStoredProc ? (CommandType?)CommandType.StoredProcedure : null);
        }

        protected virtual DynamicParameters ParameterBuilder()
        {
            return new DynamicParameters();
        }
    }
}
