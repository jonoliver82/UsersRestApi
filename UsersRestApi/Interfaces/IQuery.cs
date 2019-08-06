// **********************************************************************************
// Filename					- IQuery.cs
// Copyright (c) jonoliver82, 2019
// **********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace UsersRestApi.Interfaces
{
    public interface IQuery<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        List<Expression<Func<T, object>>> Includes { get; }

        List<string> IncludeStrings { get; }
    }
}
