using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EDIFACT;
using EDIFACT.Segments;

namespace EDIFACT.ESAP20
{
    public static class ESAP20TermMapper
    {
        

        public static LIN AddLIN(this SegmentCollection doc)
        {
            var l = new LIN();
            doc.Add(l);
            return l;
        }

    }

    public class TermDictionary
    {
        Dictionary<string, object> terms = new Dictionary<string, object>();

        public object this[string term] { get => terms[term]; set => terms[term] = value; }

        public bool TryGetValue(string term, out object obj)
        {
            return terms.TryGetValue(term, out obj);
        }



        public Dictionary<string, object> GetElementData(object dto, params string[] args)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();

            Type t = dto.GetType();
            var props = t.GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(TermAttribute)));

            foreach (var arg in args)
            {
                object o;
                try
                {
                    o = props.Where(p =>
                    {
                        var a = p.GetCustomAttributes(typeof(TermAttribute), false).Single() as TermAttribute;
                        return a.Term == arg;
                    }).Single().GetValue(dto);

                    if (o == null)
                    {
                        TryGetValue(arg, out o);
                    }
                    list.Add(arg, o);
                }
                catch (InvalidOperationException e)
                {
                    TryGetValue(arg, out o);

                    list.Add(arg, o);
                }
            }

            return list;
        }
    }
}
