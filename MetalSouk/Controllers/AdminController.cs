using Common.Library;
using Domain.Contracts;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Library.Request;

namespace MetalSouk.Controllers
{
    [Authorize(Roles="Admin")]
    [RoutePrefix("api/Admin")]
    public class AdminController : ApiController
    {

        private readonly IAdminService AdminService;
        public AdminController(IAdminService AdminService)
        {
            this.AdminService = AdminService;
        }

        [HttpPost]
        [ActionName("GetUserRoles")]
        public async Task<HttpResponseMessage> GetUserRoles()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.GetUserRoles());
        }

        [HttpPost]
        [ActionName("CreateUser")]
        public async Task<HttpResponseMessage> CreateUser(AddUserRequest newUserValues)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.AddUser(newUserValues));
        }

        [HttpPost]
        [ActionName("GetSuppliers")]
        public async Task<HttpResponseMessage> GetSuppliers()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.GetAllSuppliers());
        }

        [HttpPost]
        [ActionName("GetAgents")]
        public async Task<HttpResponseMessage> GetAgents()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.GetAllAgents());
        }

        [HttpPost]
        [ActionName("GetAnalyzers")]
        public async Task<HttpResponseMessage> GetAnalyzers()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.GetAllAnalyzers());
        }

        [HttpPost]
        [ActionName("GetUserDetails")]
        public async Task<HttpResponseMessage> GetUserDetails(int userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.GetUserDetails(userId));
        }

        [HttpPost]
        [ActionName("ModifyUser")]
        public async Task<HttpResponseMessage> ModifyUser(ModifyUserRequest userDetailValues)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.ModifyUser(userDetailValues));
        }

        [HttpPost]
        [ActionName("DisableUser")]
        public async Task<HttpResponseMessage> DisableUser(int userId)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.DisableUser(userId));
        }

        [HttpPost]
        [ActionName("GetDealDetails")]
        public async Task<HttpResponseMessage> GetDealDetails()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.DealDetails());
        }

        [HttpPost]
        [ActionName("DecideTheDeal")]
        public async Task<HttpResponseMessage> DecideTheDeal(MakeDealRequest values)
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.DecideTheDeal(values));
        }

        [HttpPost]
        [ActionName("GetAllTransactionDetails")]
        public async Task<HttpResponseMessage> GetAllTransactionDetails()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.GetDealTransactionDetails());
        }

        [HttpPost]
        [ActionName("GetUsers")]
        public async Task<HttpResponseMessage> GetUsers()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AdminService.GetUsers());
        }

        private async Task<CustomerInfromationFromToken> customerInfo()
        {
            var info = new CustomerInfromationFromToken
            {
                UserId = Convert.ToInt32(((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Sid).Value),
                Country = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Country).Value,
                FullName = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Name).Value,
                UserName = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.NameIdentifier).Value,
            };

            return info;
        }
    }

}

