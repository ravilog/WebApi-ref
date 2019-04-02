namespace Domain.Contracts
{
    using System.Threading.Tasks;
    using Common.Library;
    using Common.Library.Request;
    public interface IAdminService
    {
        Task<CommonResponse> GetUserRoles();
        Task<CommonResponse> AddUser(AddUserRequest newUserValues);
        Task<CommonResponse> GetAllSuppliers();
        Task<CommonResponse> GetAllAgents();
        Task<CommonResponse> GetAllAnalyzers();
        Task<CommonResponse> GetUserDetails(int userId);
        Task<CommonResponse> ModifyUser(ModifyUserRequest userDetailValues);
        Task<CommonResponse> DisableUser(int userId);
        Task<CommonResponse> DealDetails();
        Task<CommonResponse> DecideTheDeal(MakeDealRequest values);
        Task<CommonResponse> GetDealTransactionDetails();
        Task<CommonResponse> GetUsers();

    }
}
