using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oil.Domain.Entity.Entities;

namespace Oil.Dal.Mappings
{
    public class ShopMap : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.ToTable("Shops");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.HasOne<Company>(x => x.Company).WithMany(x => x.Shops).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany<Well>(x=>x.Wells).WithOne(x=>x.Shop).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
