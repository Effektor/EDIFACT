using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT
{
    [System.AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class TermAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        readonly string term;

        // This is a positional argument
        public TermAttribute(string term)
        {
            this.term = term;
        }

        public string Term
        {
            get { return term; }
        }

        public string Default { get; set; }
    }
}
