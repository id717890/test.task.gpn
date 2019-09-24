using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oil.Domain.Entity.Entities;

namespace Oil.Dal.Mappings
{
    public class WellTypeMap : IEntityTypeConfiguration<WellType>
    {
        public void Configure(EntityTypeBuilder<WellType> builder)
        {
            builder.ToTable("WellTypes");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
        }
    }
}
