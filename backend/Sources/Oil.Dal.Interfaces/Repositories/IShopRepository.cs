using Oil.Domain.Entity.Entities;

namespace Oil.Dal.Interfaces.Repositories
{
    public interface IShopRepository: IBaseRepository<Shop>
    {
        void DeleteRelatedWells(Shop shop);
    }
}
