using Oil.Dal.Interfaces.Repositories;
using Oil.Domain.Entity.Entities;
using System.Linq;

namespace Oil.Dal.Repositories
{
    public class ShopRepository : BaseRepository<Shop>, IShopRepository
    {
        private readonly OilDbContext _context;

        public ShopRepository(OilDbContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteRelatedWells(Shop shop)
        {
            shop.Wells.ToList().ForEach(well => shop.Wells.Remove(well));
            _context.SaveChanges();
        }
    }
}
