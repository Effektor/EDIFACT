using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Edifact_Test
{
    class Segm
    {
        ConstantExpression tag;
        Expression body;
        public Segm(string tag)
        {
            this.body = Expression.Constant(tag);
        }

        public Segm Add(string s)
        {
            return this;
        }

        public override string ToString()
        {
            return Expression.Lambda<Func<string>>(body).Compile().Invoke();
        }
    }

    class ExpressionTest
    {
        [Test]
        public void test()
        {
            //Expression.Call()
            var s = new Segm("DTM");
            Console.WriteLine(s.ToString());
        }
    }
}
