using System;
using System.Runtime.CompilerServices;

namespace Utils.Helpers
{
    public static class Extension
    {
        public static string NameOf(this object o)
        {
            return o.GetType().Name;
        }

        public static string GetMethodName(this object o, [CallerMemberName] string memberName = null)
        {
            return memberName;
        }

        public static double? GetDouble(this object value)
        {
            return double.TryParse(value.ToString(), out double lat_o) ? (double?)lat_o : null;
        }

        public static string GetString(this object value)
        {
            return value.ToString();
        }

        public static int? GetInt(this object value)
        {
            return int.TryParse(value.ToString(), out int lat_o) ? (int?)lat_o : null;
        }

        public static TimeSpan? GetTimeSpan(this object value)
        {
            return TimeSpan.TryParse(value.ToString(), out TimeSpan lat_o) ? (TimeSpan?)lat_o : null;
        }

        public static DateTime? GetDateTime(this object value)
        {
            return DateTime.TryParse(value.ToString(), out DateTime lat_o) ? (DateTime?)lat_o : null;
        }
    }
}
