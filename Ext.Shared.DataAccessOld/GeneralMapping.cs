namespace Ext.Shared.DataAccess
{
    using System;
    using System.Data;

    public static class GeneralMapping
    {
        public static Func<string, Func<IDataRecord, string>> String => col =>
        {
            return record => record.GetString(col);
        };

        public static Func<string, Func<IDataRecord, bool>> Boolean => col =>
        {
            return record => record.GetBoolean(col);
        };

        public static Func<string, Func<IDataRecord, long>> Int64 => col =>
        {
            return record => record.GetInt64(col);
        };

        public static Func<string, Func<IDataRecord, int>> Int32 => col =>
        {
            return record => record.GetInt32(col);
        };
    }
}
