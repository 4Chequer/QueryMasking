using System;
using System.Collections.Generic;
using System.Text;

namespace QueryMasking.Extension
{
    public static class StringExtension
    {
        public static bool IsEmpty(this string s)
        {
            return s == "";
        }

    }
}
