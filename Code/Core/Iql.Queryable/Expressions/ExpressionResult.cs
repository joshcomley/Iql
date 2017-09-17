﻿namespace Iql.Queryable.Expressions
{
    public class ExpressionResult<T>
    {
        public ExpressionResult(T expression = default(T))
        {
            Expression = expression;
        }

        public T Expression { get; set; }
    }
}