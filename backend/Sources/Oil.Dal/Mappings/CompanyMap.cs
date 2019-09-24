using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oil.Domain.Entity.Entities;

namespace Oil.Dal.Mappings
{
    public class CompanyMap : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Companies");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.HasMany<Field>(x => x.Fields).WithOne(x => x.Company).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany<Shop>(x => x.Shops).WithOne(x => x.Company).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
