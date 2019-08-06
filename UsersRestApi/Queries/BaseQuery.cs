// **********************************************************************************
// Filename					- BaseQuery.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Queries
{
    public abstract class BaseQuery<T> : IQuery<T>
    {
        protected BaseQuery(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        public List<string> IncludeStrings { get; } = new List<string>();

        protected virtual void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// String-based includes allow for including children of children, e.g. Basket.Items.Product.
        /// </summary>
        protected virtual void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
