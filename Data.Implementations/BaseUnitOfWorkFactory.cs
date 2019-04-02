namespace Data.Implementations
{
    using System;

    using Data.Contracts;

    public abstract class BaseUnitOfWorkFactory<T> : IUnitOfWorkFactory where T : IUnitOfWork
    {
        private readonly string connectionString;

        protected BaseUnitOfWorkFactory(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IUnitOfWork GetUnitOfWork()
        {
            return (IUnitOfWork)Activator.CreateInstance(typeof(T), this.connectionString);
        }


        public string GetConnectionString()
        {
            return this.connectionString;
        }
    }
}
