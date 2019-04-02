using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Library;

namespace Domain.Contracts
{
    public interface ISupplierService
    {
        Task<CommonResponse> AddProduct(AddProductRequest productValues, int userId);
        Task<CommonResponse> GetSupplierTransactionDetails(int userId);

    }
}
