using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace EDIFACT.Helpers
{
    public class SegmentVisitor : ExpressionVisitor
    {
        public List<object> values = new List<object>();
        private int targetElement;
        private int targetSubelement;
        private object value;

        int currentElement;
        int currentSubelement;

        public SegmentVisitor(int element)
        {
            this.targetElement = element;
        }

        public SegmentVisitor(int element, int subelement)
        {
            this.targetElement = element;
            this.targetSubelement = subelement;
        }

        public override Expression Visit(Expression node)
        {
            return base.Visit(node);
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if(node.Method.Name == "Concat" )
            {
                if (currentElement == targetElement && node.Arguments[0].NodeType == ExpressionType.Constant)
                {
                    Visit(node.Arguments[0]);
                }
                else if (node.Arguments[1] is ConstantExpression c)
                {
                    if (c.Value.ToString() == "+")
                    {
                        ++currentElement;
                        return Visit(node.Arguments[2]);
                    }
                }
            }

            return base.VisitMethodCall(node);
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            if(currentElement == targetElement)
            {
                if (node.Value.ToString().Contains(":"))
                {

                }
                else if(currentSubelement == targetSubelement)
                {
                    value = node.Value;
                    return null;
                }
            }
            return base.VisitConstant(node);
        }

        internal object GetValue(Expression node)
        {
            _ = Visit(node);
            return value;
        }
    }
}
