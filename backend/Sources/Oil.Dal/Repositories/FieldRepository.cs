using Oil.Dal.Interfaces.Repositories;
using Oil.Domain.Entity.Entities;

namespace Oil.Dal.Repositories
{
    public class FieldRepository : BaseRepository<Field>, IFieldRepository
    {
        public FieldRepository(OilDbContext context) : base(context)
        {
        }
    }
}
