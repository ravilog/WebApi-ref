using Common.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ILoginService
    {
        Task<CommonResponse> Login(LoginRequest requestValues);
    }
}
