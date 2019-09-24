using Oil.Dal.Interfaces.Repositories;
using Oil.Domain.Entity.Entities;
using System.Collections.Generic;

namespace Oil.Dal.Repositories
{
    public class WellRepository : BaseRepository<Well>, IWellRepository
    {
        private readonly OilDbContext _context;

        public WellRepository(OilDbContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteRange(IEnumerable<Well> list)
        {
            _context.Wells.RemoveRange(list);
            _context.SaveChanges();
        }
    }
}
