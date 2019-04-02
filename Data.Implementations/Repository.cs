namespace Data.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Data.Contracts;
    using System.Data.Entity;
    using System.Linq.Expressions;

    using LinqKit;

    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbset;

        public Repository(DbContext context)
        {
            this.context = context;
            this.dbset = this.context.Set<T>();
        }

        public void Add(T item)
        {
            this.dbset.Add(item);
        }

        public void AddItems(IEnumerable<T> items)
        {
            this.dbset.AddRange(items);
        }

        public void DeleteItems(IEnumerable<T> items)
        {
            this.dbset.RemoveRange(items);
        }

        public T Find(params object[] keyValues)
        {
            return this.dbset.Find(keyValues);
        }

        public IQueryable<T> SelectQuery(string query, params object[] parameters)
        {
            return this.dbset.SqlQuery(query, parameters).AsQueryable();
        }

        public void Delete(object id)
        {
            var entity = this.Find(id);
            this.Delete(entity);
        }

        public void Delete(T entity)
        {
            this.dbset.Attach(entity);
            this.dbset.Remove(entity);
        }

        public void Update(T entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
        }
        public IQueryFluent<T> Query(System.Linq.Expressions.Expression<Func<T, bool>> query)
        {
            return new QueryFluent<T>(this, query);
        }

        public IQueryFluent<T> Query()
        {
            return new QueryFluent<T>(this);
        }

        internal IQueryable<T> Select(
                                      Expression<Func<T, bool>> filter = null,
                                      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                      List<Expression<Func<T, object>>> includes = null,
                                      int? page = null,
                                      int? pageSize = null)
        {
            IQueryable<T> query = this.dbset;

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }

            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return query;
        }


    }

}
