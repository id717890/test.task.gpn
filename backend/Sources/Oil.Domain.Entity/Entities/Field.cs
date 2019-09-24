using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Oil.Domain.Entity.Entities
{
    public class Field: BaseEntity
    {
        public Company Company { get; set; }
        public Int64 CompanyId { get; set; }
        public String Name { get; set; }

        [JsonIgnore]
        public ICollection<Well> Wells { get; set; }
    }
}
