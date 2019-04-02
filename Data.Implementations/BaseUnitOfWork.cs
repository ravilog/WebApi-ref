
namespace Data.Implementations
{
    using Common.Library;
    using Data.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    public abstract class BaseUnitOfWork<TContext> : IUnitOfWork where TContext : BaseContext<TContext>
    {
        private readonly DbContext context;
        private bool disposed;
        private Dictionary<string, object> repositories;
        private DbContextTransaction transaction;

        protected BaseUnitOfWork(string connectionString)
        {
            this.context = (BaseContext<TContext>)Activator.CreateInstance(typeof(TContext), connectionString);
        }

        public DbContext GetDbContext()
        {
            return this.context;
        }

        public CommonResponse SaveChanges()
        {
            
            try
            {
                this.context.SaveChanges();
                return CommonResponse.CreateSuccess("SaveChanges", "SUC001", "");
            }            
            catch (DbEntityValidationException ex)
            {
                // Retrieve the error messages as a list of strings.
                var errorMessages = ex.EntityValidationErrors
                        .SelectMany(x => x.ValidationErrors)
                        .Select(x => x.ErrorMessage);

                // Join the list to a single string.
                var fullErrorMessage = string.Join("; ", errorMessages);

                // Combine the original exception message with the new one.
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                return CommonResponse.CreateError("SaveChanges", "ERR0001", ex.Message);
            }

            catch (DbUpdateConcurrencyException ex)
            {
                return CommonResponse.CreateError("SaveChanges", "ERR0001", ex.Message);
            }

            catch (DbUpdateException ex)
            {
                return CommonResponse.CreateError("SaveChanges", "ERR0001", ex.Message);
            }
            catch (Exception ex)
            {
                return CommonResponse.CreateError("SaveChanges", "ERR0001", ex.Message);
            }

            
        }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis", Justification = "Reviewed.")]
        public IRepository<T> Repository<T>() where T : class
        {
            if (this.repositories == null)
            {
                this.repositories = new Dictionary<string, object>();
            }

            var type = typeof(T).Name;

            if (this.repositories.ContainsKey(type))
            {
                return (IRepository<T>)this.repositories[type];
            }

            var repositoryType = typeof(Repository<>);

            this.repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), this.context));

            return (IRepository<T>)this.repositories[type];
        }

        public void BeginTransaction()
        {
            this.transaction = this.context.Database.BeginTransaction();
        }

        public CommonResponse Commit()
        {
            var CommonResponse = new CommonResponse();
            try
            {
                this.transaction.Commit();
            }
            catch (Exception ex)
            {
                CommonResponse.Error("SaveChanges", "ERR001", ex.Message);
            }

            return CommonResponse;
        }

        public void Rollback()
        {
            this.transaction.Rollback();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }
            }

            this.disposed = true;
        }
    }
}
