using Oil.Dal.Interfaces.Repositories;
using Oil.Domain.Entity.Entities;

namespace Oil.Dal.Repositories
{
    public class CompanyRepository : BaseRepository<Company>, ICompanyRepository
    {
        public CompanyRepository(OilDbContext context) : base(context)
        {
        }
    }
}
