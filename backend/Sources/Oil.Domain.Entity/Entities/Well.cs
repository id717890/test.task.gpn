using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oil.Domain.Entity.Entities
{
    public class Well: BaseEntity
    {
        /// <summary>
        /// Альтитуда
        /// </summary>
        public Int64 Altitude { get; set; }

        /// <summary>
        /// Глубина забоя искуственного
        /// </summary>
        public Int64 ZabI { get; set; }

        /// <summary>
        /// Глубина забоя фактического
        /// </summary>
        public Int64 ZabF { get; set; }

        public WellType WellType { get; set; }
        public Int64 WellTypeId { get; set; }
        public Company Company { get; set; }
        public Int64 CompanyId { get; set; }
        public Shop Shop { get; set; }
        public Int64 ShopId { get; set; }
        public Field Field { get; set; }
        public Int64 FieldId { get; set; }
        public String Name { get; set; }
    }
}
