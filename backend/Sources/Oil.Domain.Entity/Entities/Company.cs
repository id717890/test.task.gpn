using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Oil.Domain.Entity.Entities
{
    public class Company: BaseEntity
    {
        public String Name { get; set; }
        public String ShortName { get; set; }

        [JsonIgnore]
        public ICollection<Field> Fields { get; set; }

        [JsonIgnore]
        public ICollection<Shop> Shops { get; set; }

        [JsonIgnore]
        public ICollection<Well> Wells { get; set; }
    }
}
