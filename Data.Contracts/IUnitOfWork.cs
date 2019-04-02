namespace Data.Contracts
{
    using System;
    using System.Data.Entity;

    using Common.Library;

    public interface IUnitOfWork : IDisposable
    {
        CommonResponse SaveChanges();  

        void Dispose(bool disposing);

        IRepository<T> Repository<T>() where T : class;

        DbContext GetDbContext();

        void BeginTransaction();

        CommonResponse Commit(); 

        void Rollback();
    }
}
