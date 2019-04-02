namespace Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockManagement.ProductAnalyzer")]
    public partial class ProductAnalyzer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductAnalyzer()
        {
            ProductTransactions = new HashSet<ProductTransaction>();
        }

        public int Id { get; set; }

        public int SupplilerProductId { get; set; }

        public int AnalyzerId { get; set; }

        public int? AnalyzedComposition { get; set; }

        [StringLength(10)]
        public string IsDealPlaced { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        public virtual User User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductTransaction> ProductTransactions { get; set; }

        public virtual SupplierProduct SupplierProduct { get; set; }
    }
}
