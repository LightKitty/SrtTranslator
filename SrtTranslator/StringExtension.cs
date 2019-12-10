using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SrtTranslator
{
    public static class StringExtension
    {
        public static bool EndWithAny(this string str, char[] chars)
        {
            char lastChar = str[str.Length - 1];
            foreach(var c in chars)
            {
                if (c == lastChar) return true;
            }
            return false;
        }
    }
}
