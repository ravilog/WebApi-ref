using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Library;
namespace Domain.Contracts
{
    public interface IAnalyzerService
    {
        Task<CommonResponse> GetProducts();
        Task<CommonResponse> SubmitAnalyzedSample(AnalyserSampleRequest values, int userId);
    }
}
