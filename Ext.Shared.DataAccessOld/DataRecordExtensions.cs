namespace Ext.Shared.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;

    public static class DataRecordExtensions
    {
        public static int GetInt32(this IDataRecord record, string columnName)
        {
            return record.GetInt32(record.GetOrdinal(columnName));
        }

        public static int? GetNullableInt32(this IDataRecord record, string columnName)
        {
            var index = record.GetOrdinal(columnName);
            return record.IsDBNull(index) ? null : (int?)record.GetInt32(index);
        }

        public static long GetInt64(this IDataRecord record, string columnName)
        {
            return record.GetInt64(record.GetOrdinal(columnName));
        }

        public static long? GetNullableInt64(this IDataRecord record, string columnName)
        {
            var index = record.GetOrdinal(columnName);
            return record.IsDBNull(index) ? null : (long?)record.GetInt64(index);
        }

        public static decimal GetDecimal(this IDataRecord record, string columnName)
        {
            return record.GetDecimal(record.GetOrdinal(columnName));
        }

        public static decimal? GetNullableDecimal(this IDataRecord record, string columnName)
        {
            var index = record.GetOrdinal(columnName);
            return record.IsDBNull(index) ? null : (decimal?)record.GetDecimal(index);
        }

        public static bool GetBoolean(this IDataRecord record, string columnName)
        {
            return record.GetBoolean(record.GetOrdinal(columnName));
        }

        public static bool? GetNullableBoolean(this IDataRecord record, string columnName)
        {
            var index = record.GetOrdinal(columnName);
            return record.IsDBNull(index) ? null : (bool?)record.GetBoolean(index);
        }

        public static DateTimeOffset GetDateTimeOffset(this IDataRecord record, string columnName)
        {
            return (DateTimeOffset)record[record.GetOrdinal(columnName)];
        }

        public static DateTimeOffset? GetNullableDateTimeOffset(this IDataRecord record, string columnName)
        {
            var index = record.GetOrdinal(columnName);
            return record.IsDBNull(index) ? null : (DateTimeOffset?)record[index];
        }

        public static DateTime GetDateTime(this IDataRecord record, string columnName)
        {
            return record.GetDateTime(record.GetOrdinal(columnName));
        }

        public static DateTime? GetNullableDateTime(this IDataRecord record, string columnName)
        {
            var index = record.GetOrdinal(columnName);
            return record.IsDBNull(index) ? null : (DateTime?)record.GetDateTime(index);
        }

        public static string GetString(this IDataRecord record, string columnName)
        {
            var index = record.GetOrdinal(columnName);
            return record.IsDBNull(index) ? null : record.GetString(index);
        }

        public static Guid GetGuid(this IDataRecord record, string columnName)
        {
            return record.GetGuid(record.GetOrdinal(columnName));
        }

        public static Guid? GetNullableGuid(this IDataRecord record, string columnName)
        {
            var index = record.GetOrdinal(columnName);
            return record.IsDBNull(index) ? null : (Guid?)record.GetGuid(index);
        }

        public static IDictionary<string, bool> CheckColumnsExists(this IDataRecord record, params string[] columns)
        {
            if (!columns.Any())
                return new Dictionary<string, bool>();

            var result = new Dictionary<string, bool>();
            foreach (var column in columns)
                if (!result.ContainsKey(column))
                    result.Add(column, false);

            for (int i = 0; i < record.FieldCount; i++)
                if (result.ContainsKey(record.GetName(i)))
                    result[record.GetName(i)] = true;

            return result;
        }
    }
}
