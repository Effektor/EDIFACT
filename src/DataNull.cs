using System;
using System.Collections.Generic;
using System.Text;

namespace EDIFACT
{
    public class DataNull
    {
        public static readonly DataNull Value;

        static DataNull()
        {
            Value = new DataNull();
        }
    }
}
