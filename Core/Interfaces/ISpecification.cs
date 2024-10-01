using System.Linq.Expressions;

namespace Core.Interfaces
{
    // Basic specification interface for querying by Criteria and ordering.
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>>? Criteria { get; }            // Filtering criteria
        Expression<Func<T, object>>? OrderBy { get; }           // Ascending order condition
        Expression<Func<T, object>>? OrderByDescending { get; } // Descending order condition
        bool IsDistinct { get; }   
        int Take { get; }
        int Skip { get; }
        bool IsPageIsNeeded { get; }
        IQueryable<T> ApplyCriteria(IQueryable<T> query);
    }

    // Extended specification interface for supporting projections
    public interface ISpecification<T, TResult> : ISpecification<T>
    {
        Expression<Func<T, TResult>>? Select { get; }           // Projection logic
    }
}
