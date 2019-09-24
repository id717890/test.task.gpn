using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oil.Domain.Entity.Entities;

namespace Oil.Dal.Mappings
{
    public class WellMap : IEntityTypeConfiguration<Well>
    {
        public void Configure(EntityTypeBuilder<Well> builder)
        {
            builder.ToTable("Wells");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Altitude).IsRequired();
            builder.Property(x => x.ZabI).IsRequired();
            builder.Property(x => x.ZabF).IsRequired();
            builder.HasOne<WellType>(x => x.WellType);
            builder.HasOne<Company>(x => x.Company).WithMany(x => x.Wells).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Shop>(x => x.Shop).WithMany(x=>x.Wells).HasForeignKey(x=>x.ShopId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Field>(x => x.Field).WithMany(x => x.Wells).HasForeignKey(x=>x.FieldId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
