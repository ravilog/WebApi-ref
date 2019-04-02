namespace Data.Implementations.UnitOfWorks
{
    using Data.Contracts; 

    public class StockManagementUnitOfWorkFactory : BaseUnitOfWorkFactory<StockManagementUnitOfWork>, IStockManagementUnitOfWorkFactory
    {
        public StockManagementUnitOfWorkFactory(string connectionString)
            : base(connectionString)
        {

        }
    }
}
