namespace Data.Implementations.UnitOfWorks
{
    using Data.Contracts;
    using Data.Entities;

    public class StockManagementUnitOfWork : BaseUnitOfWork<StockManagementEntities>, IStockManagementUnitOfWork
    {
        public StockManagementUnitOfWork(string connectionString)
            : base(connectionString)
        {   

        }
    }
}
