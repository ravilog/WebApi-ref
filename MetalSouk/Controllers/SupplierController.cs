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

namespace MetalSouk.Controllers
{
    [Authorize(Roles="Supplier")]
    [RoutePrefix("api/Supplier")]
    public class SupplierController : ApiController
    {
        private readonly ISupplierService SupplierService;
        public SupplierController(ISupplierService SupplierService)
        {
            this.SupplierService = SupplierService;
        }

        [HttpPost]
        [ActionName("AddProduct")]
        public async Task<HttpResponseMessage> AddProduct(AddProductRequest productValues)
        {
            var info =await customerInfo();
            return Request.CreateResponse(HttpStatusCode.OK, await this.SupplierService.AddProduct(productValues, info.UserId));
        }

        [HttpPost]
        [ActionName("GetProducts")]
        public async Task<HttpResponseMessage> GetProducts()
        {
            var info = await customerInfo();
            var userId = info.UserId;
            return Request.CreateResponse(HttpStatusCode.OK, await this.SupplierService.GetSupplierTransactionDetails(userId));
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
