namespace Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public interface IRepository<T> where T : class
    {
        void Add(T item);

        void AddItems(IEnumerable<T> items);

        T Find(params object[] keyValues);

        IQueryable<T> SelectQuery(string query, params object[] parameters);

        void Update(T entity);

        void Delete(object id);

        void Delete(T entity);

        void DeleteItems(IEnumerable<T> items);

        IQueryFluent<T> Query(Expression<Func<T, bool>> query);

        IQueryFluent<T> Query();

    }
}
