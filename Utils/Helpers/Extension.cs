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
    }
}
