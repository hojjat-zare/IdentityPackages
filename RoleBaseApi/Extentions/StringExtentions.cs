using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoleBaseApi.Extentions
{
    public static class StringExtentions
    {
        public static string RemoveSubString(this string input,string subString)
        {
            return input.Replace(subString, "");
        }
    }
}
