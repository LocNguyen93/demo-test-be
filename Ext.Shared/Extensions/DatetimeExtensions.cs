namespace Ext.Shared.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        public static long ToUnixTimestampMilliseconds(this DateTime dateTime)
        {
            return (long)(dateTime.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds);
        }

        public static long ToUnixTimestampSeconds(this DateTime dateTime)
        {
            return (long)(dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        public static long? ToUnixTimestampMilliseconds(this DateTime? dateTime)
        {
            return dateTime.HasValue ? (long?)dateTime.Value.ToUnixTimestampMilliseconds() : null;
        }

        public static long? ToUnixTimestampSeconds(this DateTime? dateTime)
        {
            return dateTime.HasValue ? (long?)dateTime.Value.ToUnixTimestampSeconds() : null;
        }

        public static DateTime FromTimestampMilliSecondsToDateTime(this long timestamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime.AddMilliseconds(timestamp);
        }

        public static DateTime FromTimestampSecondsToDateTime(this long timestamp)
        {
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return dtDateTime.AddSeconds(timestamp);
        }
    }
}
