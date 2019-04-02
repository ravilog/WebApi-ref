namespace Data.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    using Data.Contracts;

    public class QueryFluent<T> : IQueryFluent<T> where T : class
    {
        private readonly Expression<Func<T, bool>> expression;
        private readonly List<Expression<Func<T, object>>> includes;
        private readonly Repository<T> repository;
        private Func<IQueryable<T>, IOrderedQueryable<T>> orderBy;

        public QueryFluent(Repository<T> repository)
        {
            this.repository = repository;
            this.includes = new List<Expression<Func<T, object>>>();
        }

        public QueryFluent(Repository<T> repository, Expression<Func<T, bool>> expression)
            : this(repository)
        {
            this.expression = expression;
        }

        public IQueryFluent<T> OrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            this.orderBy = orderBy;
            return this;
        }

        public IQueryFluent<T> Include(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            this.includes.Add(expression);
            return this;
        }

        public IQueryable<T> SelectPage(int page, int pageSize, out int totalCount)
        {
            totalCount = this.repository.Select(this.expression).Count();
            return this.repository.Select(this.expression, this.orderBy, this.includes, page, pageSize);
        }

        public IQueryable<TResult> Select<TResult>(Expression<Func<T, TResult>> selector = null)
        {
            return this.repository.Select(this.expression, this.orderBy, this.includes).Select(selector);
        }

        public IQueryable<T> Select()
        {
            return this.repository.Select(this.expression, this.orderBy, this.includes);
        }

        public IQueryable<T> SqlQuery(string query, params object[] parameters)
        {
            return this.repository.SelectQuery(query, parameters).AsQueryable();
        }
    }
}
