using Oil.Domain.Entity.Entities;
using System.Collections.Generic;

namespace Oil.Dal.Interfaces.Repositories
{
    public interface IWellRepository: IBaseRepository<Well>
    {
        void DeleteRange(IEnumerable<Well> list);
    }
}
