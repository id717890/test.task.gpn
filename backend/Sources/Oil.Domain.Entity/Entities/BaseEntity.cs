

using Oil.Domain.Interfaces.Abstract;

namespace Oil.Domain.Entity.Entities
{
    public class BaseEntity : IBaseEntity
    {
        public long Id { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var objCast = obj as BaseEntity;

            if (objCast != null)
            {
                return objCast.Id.Equals(Id);
            }

            return ReferenceEquals(obj, this);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
