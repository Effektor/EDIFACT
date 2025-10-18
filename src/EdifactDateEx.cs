using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;

namespace EDIFACT
{
    public static class EdifactDateEx
    {
        public static string ToEdiDate(this DateTime date, int formatCode)
        {
            switch (formatCode) 
            {
                case 102: return date.ToString("yyyyMMdd");
                default: throw new ArgumentException("Unknown format code");
            };
        }

        public static string Test(this DateTime t) { return t.ToString(); }
    }
}
