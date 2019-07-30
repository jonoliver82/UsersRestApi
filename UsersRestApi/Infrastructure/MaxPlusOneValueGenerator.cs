using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersRestApi.Interfaces;

namespace UsersRestApi.Infrastructure
{
    /// <summary>
    /// See https://github.com/aspnet/EntityFrameworkCore/issues/6872
    /// Work around an issue with InMemoryProvider not taking into account seeded data
    /// when generating a new identity
    /// </summary>    
    public class MaxPlusOneValueGenerator<TEntity> : ValueGenerator<int>
        where TEntity : class, IIdentifiable
    {
        public override bool GeneratesTemporaryValues => false;

        public override int Next(EntityEntry entry)
        {
            var data = entry.Context.Set<TEntity>();
            var max = 0;
            if (data.Any())
            {
                max = data.Max(a => a.Id);
            }
            return max + 1;
        }
    }
}
