namespace Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockManagement.SupplierProducts")]
    public partial class SupplierProduct
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SupplierProduct()
        {
            ProductAnalyzers = new HashSet<ProductAnalyzer>();
        }

        public int Id { get; set; }

        public int ProductId { get; set; }

        public int SupplierId { get; set; }

        public decimal? Quantity { get; set; }

        public int? Composition { get; set; }

        [StringLength(1)]
        public string AvailableStatus { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAnalyzer> ProductAnalyzers { get; set; }

        public virtual Product Product { get; set; }

        public virtual User User { get; set; }
    }
}
