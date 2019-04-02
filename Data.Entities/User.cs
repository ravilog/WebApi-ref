namespace Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("StockManagement.Users")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            ProductAnalyzers = new HashSet<ProductAnalyzer>();
            SupplierProducts = new HashSet<SupplierProduct>();
            AgentSuppliers = new HashSet<AgentSupplier>();
            AgentSuppliers1 = new HashSet<AgentSupplier>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string FullName { get; set; }

        [Required]
        [StringLength(30)]
        public string UserName { get; set; }

        [Required]
        [StringLength(128)]
        public string UserPassword { get; set; }

        [StringLength(50)]
        public string EmailId { get; set; }

        [StringLength(30)]
        public string Country { get; set; }

        [StringLength(30)]
        public string State { get; set; }

        [StringLength(128)]
        public string PhoneNumber { get; set; }

        public int UserRole { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? UpdatedDate { get; set; }

        [StringLength(30)]
        public string CreateBy { get; set; }

        [StringLength(30)]
        public string UpdatedBy { get; set; }

        public bool? IsActive { get; set; }

        [StringLength(10)]
        public string IsDiabled { get; set; }

        [StringLength(100)]
        public string City { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductAnalyzer> ProductAnalyzers { get; set; }

        public virtual Role Role { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SupplierProduct> SupplierProducts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentSupplier> AgentSuppliers { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AgentSupplier> AgentSuppliers1 { get; set; }
    }
}
