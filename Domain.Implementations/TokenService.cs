namespace Domain.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using System.Web;
    using Domain.Contracts;
    using Data.Entities;
    using Data.Contracts;
    using System.Data;
    using Common.Library;
    using System.Data.Entity;
    using Domain.Entities;

    public class TokenService : ITokenService
    {
        private IStockManagementUnitOfWorkFactory unitOfWorkFactory;
        public TokenService(IStockManagementUnitOfWorkFactory unitOfWorkFactory)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<string> GenerateToken(string userName, string password)
        {
            var pairs = new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>( "grant_type", "password" ),
                            new KeyValuePair<string, string>( "username", userName ),
                            new KeyValuePair<string, string> ( "Password", password )
                        };
            var content = new FormUrlEncodedContent(pairs);
            using (var client = new HttpClient())
            {
                var scheme = HttpContext.Current.Request.Url.Scheme;
                var host = HttpContext.Current.Request.Url.Host;
                var uri_builder = new UriBuilder();

                if (ConfigurationManager.AppSettings["Debug"].ToString() == "on")
                {
                    uri_builder = new UriBuilder(scheme, host, Convert.ToInt32(ConfigurationManager.AppSettings["Port"]));
                }
                else
                {
                    uri_builder = new UriBuilder(scheme, host);
                }

                var response =
                    client.PostAsync(uri_builder + ConfigurationManager.AppSettings["tokendomain"], content).Result;
                return await response.Content.ReadAsStringAsync();
            }
        }



        public async Task<CustomerInfromationFromToken> ValidateUser(string userName, string password)
        {
            using (var uow = this.unitOfWorkFactory.GetUnitOfWork())
            {

                var loginData = await uow.Repository<User>().Query().Select().FirstOrDefaultAsync(a =>
                                         a.UserName == userName && a.UserPassword == password);
                if (loginData == null)
                {
                    return new CustomerInfromationFromToken();
                }
                CustomerInfromationFromToken response = new CustomerInfromationFromToken
                {
                    UserName = loginData.UserName,
                    Country = loginData.Country,
                    FullName = loginData.FullName,
                    PhoneNumber = loginData.PhoneNumber,
                    State = loginData.State,
                    UserId = loginData.Id

                };
                return response;
            }
        }
    }
}
