using Common.Library;
using Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MetalSouk.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        private readonly ILoginService loginService;
        public LoginController(ILoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        [ActionName("GetLogin")]
        public async Task<HttpResponseMessage> GetLogin(LoginRequest Values)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return Request.CreateResponse(HttpStatusCode.OK, await this.loginService.Login(Values));
        }

    }
}
