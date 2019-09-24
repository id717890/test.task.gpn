using Oil.Dal.Interfaces.Repositories;
using Oil.Domain.Entity.Entities;

namespace Oil.Dal.Repositories
{
    public class WellTypeRepository : BaseRepository<WellType>, IWellTypeRepository
    {

        public WellTypeRepository(OilDbContext context) : base(context)
        {
        }
    }
}
