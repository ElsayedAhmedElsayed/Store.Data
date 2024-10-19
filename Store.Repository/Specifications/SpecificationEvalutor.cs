using Microsoft.EntityFrameworkCore;
using Store.Data.Enity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Repository.Specifications
{
    public class SpecificationEvalutor<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecification<TEntity> Specs)
        {
            var query = InputQuery;

            if (Specs.Criteria is not null)
            {
                query = query.Where(Specs.Criteria);
            }

            if(Specs.OrderBy is not null)
            {
                query = query.OrderBy(Specs.OrderBy);
            }

            if (Specs.OrderByDescending is not null)
            {
                query = query.OrderBy(Specs.OrderByDescending);
            }

            if(Specs.IsPaginated)
            {
                query = query.Skip(Specs.Skip).Take(Specs.Take);
            }

            query = Specs.Includes.Aggregate(query, (Current, includeEx) => Current.Include(includeEx));

            return query;
        }
    }
}
