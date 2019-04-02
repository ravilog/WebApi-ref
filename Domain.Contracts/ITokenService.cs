namespace Domain.Contracts
{
    using Common.Library;
    using Data.Entities;
    using Domain.Entities;
    using System.Data;
    using System.Threading.Tasks;

    public interface ITokenService
    {
        Task<string> GenerateToken(string userName, string password);

        Task<CustomerInfromationFromToken> ValidateUser(string userName, string password);
    }
}
