namespace Data.Entities
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Data.Implementations;

    public partial class StockManagementEntities : BaseContext<StockManagementEntities>
    {
        public StockManagementEntities(string connectionString)
            : base(connectionString)
        {
        }

        public virtual DbSet<ProductAnalyzer> ProductAnalyzers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductTransaction> ProductTransactions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SupplierProduct> SupplierProducts { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<AgentSupplier> AgentSuppliers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductAnalyzer>()
                .Property(e => e.IsDealPlaced)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ProductAnalyzer>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<ProductAnalyzer>()
                .HasMany(e => e.ProductTransactions)
                .WithRequired(e => e.ProductAnalyzer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.ProductName)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.SupplierProducts)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ProductTransaction>()
                .Property(e => e.Deal)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .Property(e => e.RoleName)
                .IsUnicode(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .HasForeignKey(e => e.UserRole)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<SupplierProduct>()
                .Property(e => e.Quantity)
                .HasPrecision(18, 0);

            modelBuilder.Entity<SupplierProduct>()
                .Property(e => e.AvailableStatus)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<SupplierProduct>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<SupplierProduct>()
                .HasMany(e => e.ProductAnalyzers)
                .WithRequired(e => e.SupplierProduct)
                .HasForeignKey(e => e.SupplilerProductId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FullName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UserName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Country)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.State)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.CreateBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.UpdatedBy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.IsDiabled)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ProductAnalyzers)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AnalyzerId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.SupplierProducts)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.SupplierId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.AgentSuppliers)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.AgentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.AgentSuppliers1)
                .WithRequired(e => e.User1)
                .HasForeignKey(e => e.SupplierId)
                .WillCascadeOnDelete(false);
        }
    }
}
