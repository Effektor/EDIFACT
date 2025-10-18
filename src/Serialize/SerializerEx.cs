using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT.Serialize
{
    public static class SerializerEx
    {
        public static void AddSerialized(this IList<string> collection, object obj)
        {
            if(obj is System.Collections.IEnumerable row)
            {
                var enumerator = row.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    collection.Add(EdiObject.SerializeObject(enumerator.Current));
                }
            }
            else
            {
                collection.Add(EdiObject.SerializeObject(obj));
            }

            
        }

        public static void Add(this List<dynamic> list, dynamic o)
        {

        }
    }
}
