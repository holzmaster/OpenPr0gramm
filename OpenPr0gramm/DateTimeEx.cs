using System;

namespace OpenPr0gramm
{
    /// <summary>Some utility extensions on <typeparam name="DateTime"/>.</summary>
    internal static class DateTimeExtensions
    {
        public static long ToUnixTime(this DateTime value)
        {
            var totalSeconds = (value.ToUniversalTime() - DateTimeEx.UnixStart).TotalSeconds;
            return (long)Math.Truncate(totalSeconds);
        }
    }

    internal static class DateTimeEx
    {
        internal static readonly DateTime UnixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public static DateTime FromUnixToUtcDateTime(int unixTime) => FromUnixToUtcDateTime((long)unixTime);
        public static DateTime FromUnixToUtcDateTime(long unixTime) => UnixStart.AddSeconds(unixTime);


        // TODO: Tests
        public static DateTime FromUnixToLocalTime(int unixTime) => FromUnixToLocalTime((long)unixTime);
        public static DateTime FromUnixToLocalTime(long unixTime) => FromUnixToUtcDateTime(unixTime).ToLocalTime();
    }
}
