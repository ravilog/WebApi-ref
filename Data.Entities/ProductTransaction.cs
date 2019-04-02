namespace Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockManagement.ProductTransaction")]
    public partial class ProductTransaction
    {
        public int Id { get; set; }

        public int ProductAnalyzerId { get; set; }

        [StringLength(1)]
        public string Deal { get; set; }

        public virtual ProductAnalyzer ProductAnalyzer { get; set; }
    }
}
