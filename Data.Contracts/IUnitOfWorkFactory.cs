namespace Data.Contracts
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork GetUnitOfWork();
        string GetConnectionString();
    }
}
