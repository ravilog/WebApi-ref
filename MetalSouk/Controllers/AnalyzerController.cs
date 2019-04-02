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
    [Authorize(Roles ="Analyzer")]
    [RoutePrefix("api/Analyzer")]
    public class AnalyzerController : ApiController
    {
        private readonly IAnalyzerService AnalyzerService;
        public AnalyzerController(IAnalyzerService AnalyzerService)
        {
            this.AnalyzerService = AnalyzerService;
        }

        [HttpPost]
        [ActionName("GetProducts")]
        public async Task<HttpResponseMessage> GetProducts()
        {
            return Request.CreateResponse(HttpStatusCode.OK, await this.AnalyzerService.GetProducts());
        }

        [HttpPost]
        [ActionName("SubmitAnalysis")]
        public async Task<HttpResponseMessage> SubmitAnalysis(AnalyserSampleRequest values)
        {
            var info = await customerInfo();
            return Request.CreateResponse(HttpStatusCode.OK, await this.AnalyzerService.SubmitAnalyzedSample(values, info.UserId));
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
