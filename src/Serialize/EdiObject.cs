using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Dynamic;

namespace EDIFACT.Serialize
{
    public class EdiObject
    {
        public static string SerializeObject(object o)
        {

            if (o is string ss) return ss;

            var anonType = o.GetType();

            if (o is Array a) return string.Join("", a.Cast<object>().ToList().Select(x => SerializeObject(x)));
            else if (o is IEnumerable<object> e) return string.Join("", e.Select(x => SerializeObject(x)));

            var props = anonType.GetProperties();

            string tag = anonType.GetProperty("Tag") != null ? anonType.GetProperty("Tag")?.GetValue(o).ToString() 
                : props.Count() > 0 ? props.FirstOrDefault()?.GetValue(o).ToString() 
                : throw new ArgumentException("Can't infer Tag property");

            List<string> elements = new List<string>();

            foreach (var prop in props)
            {
                object val = prop.GetValue(o);


                if (val is string s)
                {
                    if (s == tag) continue;
                    else if (string.IsNullOrWhiteSpace(s)) elements.Add("");
                    else elements.Add(s);
                }
                else if (val is Array) elements.Add(SerializeComposite(val));
                else if (val == null) elements.Add("");
                else elements.Add(val.ToString());

            }

            return $"{tag}+{string.Join("+",elements)}'";
        }

        private static string SerializeComposite(object val)
        {
            IEnumerable<object> subelements = val as IEnumerable<object>;
            return string.Join(":", subelements.Select(x => x?.ToString() ?? ""));
        }


    }
}
