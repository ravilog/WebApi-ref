using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class DealsDetailsDTO
    {
        public int ProductAnalyzerId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public int Composition { get; set; }
        public int AnalyzedComposition { get; set; }
        public string SupplierName { get; set; }
    }
}
