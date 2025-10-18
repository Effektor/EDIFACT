using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace EDIFACT
{

    public class Segment
    {
        private readonly string tag;
        public string Tag => tag;

        static ConstantExpression segmentSeparator = Expression.Constant("+");

        public object this[int element] { get
            {
                var visitor = new Helpers.SegmentVisitor(element);
                return visitor.GetValue(this.body);
            } }

        Expression body;

        public Segment(string tag)
        {
            this.body = Expression.Constant(tag);
            this.tag = tag;
        }

        //Needed for unit testing
        public Segment()
        {

        }

        public Segment AddElement(object data, bool allowNull = false)
        {
            if (allowNull && data == null) data = DataNull.Value;
            return AddElement(data);
        }

        public Segment AddElement(object s)
        {
            if (s == null) throw new ArgumentNullException(nameof(s));
            if (s == DataNull.Value) s = "";
            return AddElement(Expression.Constant(s,typeof(Object)));
        }

        public virtual Segment AddElement(Expression expr)
        {
            MethodInfo m = typeof(string).GetMethod("Concat", new Type[] { typeof(Expression), typeof(string), typeof(string) });
            body = Expression.Call(null, m, body, segmentSeparator, expr);

            return this;
        }

        public virtual Segment AddElement()
        {
            return AddElement("");
        }

        

        public virtual Segment AddComposite(params object[] obj)
        {
            
            while(obj.Length > 0 && obj[obj.Length -1] == DataNull.Value)
            {
                obj = obj.Take(obj.Length - 1).ToArray();
            }

            /// I cant remember why we desided to reject nulls and require explicit DataNull.Value.
            /// Will allow nulls for now, revert back if it causes problems.
            /// Throwing an exception here makes testing more difficult since we dont know which mapped object is calling in here.

            for (int i = 0; i < obj.Length; i++)
            {
                /*
                if (obj[i] == null)
                    throw new ArgumentNullException(nameof(obj), "Can't have null arguments. Use DataNull intead");
                else if (obj[i] == DataNull.Value) obj[i] = "";*/
                if (obj[i] == null || obj[i] == DataNull.Value) obj[i] = "";
            }

            if (obj.Length == 0) return AddElement();
            
            var arr = Expression.Constant(obj, typeof(object[]));
            MethodInfo m = typeof(string).GetMethod("Join", new Type[] {typeof(string), typeof(object[]) });
            var call = Expression.Call( m, Expression.Constant(":"), arr );
            return AddElement(call);
        }

        public override string ToString()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.InvariantCulture;
            return Helpers.SegmentHelpers.TrimSegment(Expression.Lambda<Func<string>>(body).Compile().Invoke() +"'");
        }
        

        public Segment AddElements(params object[] args)
        {
            foreach (object arg in args) AddElement(arg);
            return this;
        }

        internal static Segment SetUNA(string componentDataElementSeparator, string dataElementSeparator, string decimalNotation, string releaseIndicator, string reservedForFutureUse, string segmentTerminator)
        {
            var una = new UNA(componentDataElementSeparator,
               dataElementSeparator,
               decimalNotation,
               releaseIndicator,
               reservedForFutureUse,
               segmentTerminator);

            return una;
        }

        internal Expression GetExpression() => body;


        class UNA : Segment
        {
            public readonly string componentDataElementSeparator;
            public readonly string dataElementSeparator;
            public readonly string decimalNotation;
            public readonly string releaseIndicator;
            public readonly string reservedForFutureUse;
            public readonly string segmentTerminator;

            public UNA(string componentDataElementSeparator, 
                string dataElementSeparator, 
                string decimalNotation, 
                string releaseIndicator, 
                string reservedForFutureUse, 
                string segmentTerminator) 
                : base("UNA")
            {
                this.componentDataElementSeparator = componentDataElementSeparator;
                this.dataElementSeparator = dataElementSeparator;
                this.decimalNotation = decimalNotation;
                this.releaseIndicator = releaseIndicator;
                this.reservedForFutureUse = reservedForFutureUse;
                this.segmentTerminator = segmentTerminator;
            }

            public override string ToString()
            {
                return string.Format("UNA{0}{1}{2}{3}{4}{5}", componentDataElementSeparator,
               dataElementSeparator,
               decimalNotation,
               releaseIndicator,
               reservedForFutureUse,
               segmentTerminator);
            }
        }
    }
}
