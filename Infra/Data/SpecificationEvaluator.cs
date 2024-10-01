using Core.Entites;
using Core.Interfaces;

namespace Infra.Data;

public class SpecificationEvaluator<T> where T : BaseEntity
{
    public static IQueryable<T> GetQuery(IQueryable<T> query, ISpecification<T> spec)
    {
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        if (spec.IsDistinct)
        {
            query = query.Distinct();

        }

        if (spec.IsPageIsNeeded)
        {
            query = query.Skip(spec.Skip).Take(spec.Take);
        }
        return query;
    }

    public static IQueryable<TResult> GetQuery<TSpec, TResult>(IQueryable<T> query, ISpecification<T, TResult> spec)
    {
        // Check if Criteria is not null, then apply the filter
        if (spec.Criteria != null)
        {
            query = query.Where(spec.Criteria);
        }

        // Check if OrderBy is not null, then apply ordering
        if (spec.OrderBy != null)
        {
            query = query.OrderBy(spec.OrderBy);
        }

        // Check if OrderByDescending is not null, then apply descending ordering
        if (spec.OrderByDescending != null)
        {
            query = query.OrderByDescending(spec.OrderByDescending);
        }

        // Cast the query to IQueryable<TResult>
        var selectQuery = query as IQueryable<TResult>;

        // Check if Select is not null, then apply the projection
        if (spec.Select != null)
        {
            selectQuery = query.Select(spec.Select);  // Assuming spec.Select is of type Expression<Func<T, TResult>>
        }
        if (spec.IsDistinct)
        {
            selectQuery = selectQuery!.Distinct();
        }
        if (spec.IsPageIsNeeded)
        {
            selectQuery = selectQuery!.Skip(spec.Skip).Take(spec.Take);
        }

        // Return the selectQuery, or if null, cast the original query to TResult
        return selectQuery ?? query.Cast<TResult>();
    }

}
