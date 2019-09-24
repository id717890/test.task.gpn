using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oil.Domain.Entity.Entities;

namespace Oil.Dal.Mappings
{
    public class FieldMap : IEntityTypeConfiguration<Field>
    {
        public void Configure(EntityTypeBuilder<Field> builder)
        {
            builder.ToTable("Fields");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).IsRequired();
            builder.HasOne<Company>(x => x.Company);
            builder.HasMany<Well>(x => x.Wells).WithOne(x => x.Field).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
