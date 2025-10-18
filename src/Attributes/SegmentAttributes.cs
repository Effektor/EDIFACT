using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    public sealed class EdiSegmentAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public string Tag { get; }

        // This is a positional argument
        public EdiSegmentAttribute(string tag)
        {
            this.Tag = tag;
        }

        public string PositionalString
        {
            get { return Tag; }
        }
    }
}
