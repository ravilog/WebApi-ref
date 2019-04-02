using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Library;
using Data.Contracts;
using Common.Library.Response;

namespace Domain.Implementations
{
    public class LoginService : ILoginService
    {
        private IStockManagementUnitOfWorkFactory unitOfWorkFactory;
        private ITokenService tokenService;
        public LoginService(IStockManagementUnitOfWorkFactory unitOfWorkFactory, ITokenService tokenService)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.tokenService = tokenService;
        }
        public async Task<CommonResponse> Login(LoginRequest requestValues)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {
                var tokenResponse = await this.tokenService.GenerateToken(requestValues.UserName, requestValues.Password);
                if (tokenResponse.ToLower().Contains("error"))
                {
                    return CommonResponse.CreateError("Invalid user credentials, please try again.", "ERR001", "");
                }
                dynamic newToken = Newtonsoft.Json.JsonConvert.DeserializeObject(tokenResponse);
                string tokenValue = "Bearer " + newToken.access_token;

                LoginResponse loginResponse = new LoginResponse();
                loginResponse.token = tokenValue;
                loginResponse.UserFullName = newToken.FullName;
                return CommonResponse.CreateSuccess("Logged in successfully.", "SUC001", loginResponse);

            }
        }
    }
}
