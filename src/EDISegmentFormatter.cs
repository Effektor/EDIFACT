using System;
using System.Collections.Generic;
using System.Text;
using EDIFACT;
using System.Linq;
using System.Reflection;

namespace EDIFACT
{
    class EDISegmentFormatter
    {
        internal string Format(IEdifactSegment segment)
        {
            var type = segment.GetType();
            var segmentAttribute = type.GetCustomAttribute(typeof(EdiSegmentAttribute)) as EdiSegmentAttribute;
            if (segmentAttribute == null) return segment.ToString();

            string ediString = $"{segmentAttribute.Tag}";

            //Get properties to serialize
            var props = type.GetProperties();
            List<(string Path, PropertyInfo)> elements = new List<(string, PropertyInfo)>();
            foreach(var prop in props)
            {
                var compositeAttribute = prop.GetCustomAttribute(typeof(DataElementAttribute)) as DataElementAttribute;
                if (compositeAttribute == null) continue;
                elements.Add((compositeAttribute.Path, prop));
            }



            return ediString;
            
        }
    }
}
