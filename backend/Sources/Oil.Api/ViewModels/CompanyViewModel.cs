using Newtonsoft.Json;
using System;

namespace Oil.Api.ViewModels
{
    public class CompanyViewModel
    {
        public class CompanyView
        {
            [JsonProperty("id")]
            public Int64 Id { get; set; }

            [JsonProperty("name")]
            public String Name { get; set; }

            [JsonProperty("shortName")]
            public String ShortName { get; set; }
        }
    }
}
