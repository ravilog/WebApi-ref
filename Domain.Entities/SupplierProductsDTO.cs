using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SupplierProductsDTO
    {
        public int SupplierProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Quantity { get; set; }
        public int Composition { get; set; }
    }
}
