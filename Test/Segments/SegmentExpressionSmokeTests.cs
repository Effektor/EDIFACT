using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace EDIFACT.Tests.Segments
{
    internal class SegmentExpressionSmokeTests
    {
        private class Segm
        {
            private readonly Expression body;

            public Segm(string tag)
            {
                body = Expression.Constant(tag);
            }

            public override string ToString()
            {
                return Expression.Lambda<Func<string>>(body).Compile().Invoke();
            }
        }

        [Test]
        public void EmitsTagWhenConvertingToString()
        {
            var segment = new Segm("DTM");

            Assert.That(segment.ToString(), Is.EqualTo("DTM"));
        }
    }
}
