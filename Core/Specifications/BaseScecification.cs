using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseScecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{

    protected BaseScecification() : this(null) { }
    public Expression<Func<T, bool>>? Criteria => criteria;

    public Expression<Func<T, object>>? OrderBy { get; private set; }

    public Expression<Func<T, object>>? OrderByDescending { get; private set; }

    public bool IsDistinct { get; private set; }

    public int Take { get; private set; }

    public int Skip { get; private set; }

    public bool IsPageIsNeeded { get; private set; }

    public IQueryable<T> ApplyCriteria(IQueryable<T> query)
    {
        if (criteria != null)
        {
            query = query.Where(criteria);
        }
        return query;
    }


    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDecExpression)
    {
        OrderByDescending = orderByDecExpression;
    }

    protected void ApplyDistint()
    {
        IsDistinct = true;
    }
    protected void ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPageIsNeeded = true;
    }

}
public class BaseScecification<T, TResult>(Expression<Func<T, bool>>? criteria) : BaseScecification<T>(criteria), ISpecification<T, TResult>
{
    protected BaseScecification() : this(null) { }
    public Expression<Func<T, TResult>>? Select { get; private set; }

    protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
    {
        Select = selectExpression;
    }

}