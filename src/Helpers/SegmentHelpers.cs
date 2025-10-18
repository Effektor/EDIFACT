using EDIFACT.Segments;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
[assembly: InternalsVisibleTo("Edifact_Test")]
namespace EDIFACT.Helpers
{
    class SegmentHelpers
    {
        public static string TrimSegment(string segmentString)
        {
            segmentString = Regex.Replace(segmentString, "\\++'$", "'");

            return segmentString;
        }

        internal static decimal GetQtyValue(Segment qty)
        {
            string seg = qty.ToString();
            var match = Regex.Match(seg, @"^QTY\+\d+:([\d\.]+)(?:.+)*'$");
            decimal.TryParse(match.Groups[1].Value, out decimal d);
            return d;
        }
    }
}
