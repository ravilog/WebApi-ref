using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductsTransactionDetailsDTO
    {
        public string ProductName { get; set; }
        public int Composition { get; set; }
        public int AnalyzedComposition { get; set; }
        public string supplierName { get; set; }
        public int ProductTransactionId { get; set; }
        public string DealStatus { get; set; }
    }
}
