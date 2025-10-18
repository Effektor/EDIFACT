using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EDIFACT.Helpers;

namespace EDIFACT.ESAP20
{
    public class DESADVMessage : EDIMessage
    {
        IEnumerable<Segment> GetControlSums(SegmentCollection segments)
        {
            decimal ControlQuantity = segments.Where(s => s.Tag == "QTY").Sum(qty => Helpers.SegmentHelpers.GetQtyValue(qty));
            int LineCount = segments.Where(s => s.Tag == "LIN").Count();
            int SegmentCount = segments.Count();

            for (int i = SegmentCount - 1; i > 0; --i)
            {
                if (segments[i].Tag == "BGM")
                {
                    SegmentCount -= i;
                    break;
                }
            }

            return Helpers.Interchange.GetMessageTrailer(ControlQuantity, LineCount);
        }

        internal override IEnumerable<Segment> FullMessageEnumerator()
        {
            yield return GetUNH();

            foreach (Segment seg in this)
            {
                yield return seg;
            }
            foreach(Segment seg in GetControlSums(this))
            {
                yield return seg;
            }
            yield return Interchange.GetUNT(this.Count()+4, this.MessageReferenceNumber);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach(var s in FullMessageEnumerator())
            {
                stringBuilder.Append(s.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
