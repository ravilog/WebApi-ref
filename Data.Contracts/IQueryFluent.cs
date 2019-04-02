namespace Data.Contracts
{

    using System;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IQueryFluent<T> where T : class
    {
        IQueryFluent<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy);

        IQueryFluent<T> Include(Expression<Func<T, object>> expression);

        IQueryable<T> SelectPage(int page, int pageSize, out int totalCount);

        IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector = null);

        IQueryable<T> Select();

        IQueryable<T> SqlQuery(string query, params object[] parameters);
    }
}
