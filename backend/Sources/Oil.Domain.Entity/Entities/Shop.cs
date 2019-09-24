using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Oil.Domain.Entity.Entities
{
    public class Shop: BaseEntity
    {
        public Company Company { get; set; }
        public Int64 CompanyId { get; set; }
        public String Name { get; set; }

        [JsonIgnore]
        public ICollection<Well> Wells { get; set; }
    }
}
