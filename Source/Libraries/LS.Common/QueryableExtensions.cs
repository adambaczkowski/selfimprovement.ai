using System;
using System.Linq;
using System.Linq.Expressions;

namespace LS.Common
{
    public static class QueryableExtensions
    {
        /// <summary>
        /// Applies .Where() method if given filter string is not empty and not null
        /// </summary>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, string filter, Expression<Func<T, bool>> predicate) =>
            string.IsNullOrEmpty(filter)
                ? query
                : query.Where(predicate);

        /// <summary>
        /// Applies .Where() method if given condition is satisfied
        /// </summary>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate) =>
            condition
                ? query.Where(predicate)
                : query;
    }
}