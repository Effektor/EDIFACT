using System;

namespace EDIFACT
{
    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class DataElementAttribute : Attribute
    {
        private readonly string path;

        public DataElementAttribute(string Path)
        {
            this.path = Path;
        }

        public string Path => path;
        public bool Mandatory { get; set; }

        public string Format { get; set; }

    }
    
    
}
