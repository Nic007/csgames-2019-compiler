using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.eShopWeb.ApplicationCore.Entities;
using Microsoft.eShopWeb.ApplicationCore.Entities.BasketAggregate;
using Microsoft.eShopWeb.ApplicationCore.Entities.OrderAggregate;

namespace Microsoft.eShopWeb.Infrastructure.Data
{

    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {
        }

        public DbSet<Basket> Baskets { get; set; } // @issue@I02
        public DbSet<CatalogItem> catalogItems { get; set; } // @issue@I01 @issue@I02
        public DbSet<CatalogBrand> CatalogBrands { get; set; } // @issue@I02
        public DbSet<CatalogType> CatalogTypes { get; set; } // @issue@I02
        public DbSet<Order> Orders { get; set; } // @issue@I02
        public DbSet<OrderItem> OrderItems { get; set; } // @issue@I02

        protected override void OnModelCreating(ModelBuilder builder) // @issue@I02
        {
            builder.Entity<Basket>(ConfigureBasket); // @issue@I02
            builder.Entity<CatalogBrand>(ConfigureCatalogBrand); // @issue@I02
            builder.Entity<CatalogType>(ConfigureCatalogType); // @issue@I02
            builder.Entity<CatalogItem>(ConfigureCatalogItem); // @issue@I02
            builder.Entity<Order>(ConfigureOrder); // @issue@I02
            builder.Entity<OrderItem>(ConfigureOrderItem); // @issue@I02
        }

        private void ConfigureBasket(EntityTypeBuilder<Basket> builder) // @issue@I02
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Basket.Items)); // @issue@I02

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field); // @issue@I02
        }

        private void ConfigureCatalogItem(EntityTypeBuilder<CatalogItem> builder) // @issue@I02
        {
            builder.ToTable("Catalog"); // @issue@I02

            builder.Property(ci => ci.Id) // @issue@I02
                .ForSqlServerUseSequenceHiLo("catalog_hilo")
                .IsRequired();

            builder.Property(ci => ci.Name) // @issue@I02
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(ci => ci.Price) // @issue@I02
                .IsRequired(true);

            builder.Property(ci => ci.PictureUri) // @issue@I02
                .IsRequired(false);

            builder.HasOne(ci => ci.CatalogBrand) // @issue@I02
                .WithMany()
                .HasForeignKey(ci => ci.CatalogBrandId);

            builder.HasOne(ci => ci.CatalogType) // @issue@I02
                .WithMany()
                .HasForeignKey(ci => ci.CatalogTypeId);
        }

        private void ConfigureCatalogBrand(EntityTypeBuilder<CatalogBrand> builder) // @issue@I02
        {
            builder.ToTable("CatalogBrand"); // @issue@I02

            builder.HasKey(ci => ci.Id); // @issue@I02

            builder.Property(ci => ci.Id) // @issue@I02
               .ForSqlServerUseSequenceHiLo("catalog_brand_hilo")
               .IsRequired();

            builder.Property(cb => cb.Brand) // @issue@I02
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureCatalogType(EntityTypeBuilder<CatalogType> builder) // @issue@I02
        {
            builder.ToTable("CatalogType"); // @issue@I02

            builder.HasKey(ci => ci.Id); // @issue@I02

            builder.Property(ci => ci.Id) // @issue@I02
               .ForSqlServerUseSequenceHiLo("catalog_type_hilo")
               .IsRequired();

            builder.Property(cb => cb.Type) // @issue@I02
                .IsRequired()
                .HasMaxLength(100);
        }

        private void ConfigureOrder(EntityTypeBuilder<Order> builder) // @issue@I02
        {
            var navigation = builder.Metadata.FindNavigation(nameof(Order.OrderItems)); // @issue@I02

            navigation.SetPropertyAccessMode(PropertyAccessMode.Field); // @issue@I02

            builder.OwnsOne(o => o.ShipToAddress); // @issue@I02
        }

        private void ConfigureOrderItem(EntityTypeBuilder<OrderItem> builder) // @issue@I02
        {
            builder.OwnsOne(i => i.ItemOrdered); // @issue@I02
        }
    }
}
