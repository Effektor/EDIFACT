using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;

namespace EDIFACT.Segments
{
    public class DTM : Segment
    {
        private string dateFormat = "yyyyMMdd";

        public DTM() : base("DTM")
        {

        }

        public new DTM AddComposite(params object[] obj)
        {
           
                Expression<Func<DateTime, string>> format = (dt) => dt.ToString(dateFormat);
                var altered = obj.Select(x => x is DateTime dt ? format.Compile().Invoke(dt) : x);
                base.AddComposite(altered.ToArray());
                return this;
           
        }

        public DTM WithDateformat(string dateformat)
        {
            this.dateFormat = dateformat;
            return this;
        }
    }
}
