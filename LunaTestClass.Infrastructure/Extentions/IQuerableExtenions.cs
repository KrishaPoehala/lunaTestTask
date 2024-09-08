using System.Linq.Expressions;

namespace LunaTestTask.Infrastructure.Extentions;

public static class IQuerableExtenions
{
    //concise way of sorting since linq has two distinct methods for sorting
    public static IQueryable<T> OrderBy<T,TKey>(this IQueryable<T> query,
        Expression<Func<T,TKey>> expression, bool order)
    {
        if (order)
        {
            return query.OrderBy(expression);
        }

        return query.OrderByDescending(expression);
    }
}
