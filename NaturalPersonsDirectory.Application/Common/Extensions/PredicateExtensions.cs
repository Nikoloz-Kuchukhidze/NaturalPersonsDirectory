using System.Linq.Expressions;

namespace NaturalPersonsDirectory.Application.Common.Extensions;

public static class PredicateExtensions
{
    public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> left,  Expression<Func<T, bool>> right)
    {
        var andAlsoExpression = Expression.AndAlso(left.Body, Expression.Invoke(right, left.Parameters[0]));
        return Expression.Lambda<Func<T, bool>>(andAlsoExpression, left.Parameters);
    }
}
