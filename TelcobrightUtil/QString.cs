using System;
using System.Text;

namespace TelcobrightUtil
{
    public static class QString
    {
        public static string Base64Encode(this string s)
        {
            if (s == null) s = "";
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }

        public static string Base64Decode(this string s)
        {
            if (s == null) s = "";
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }
    }
}
