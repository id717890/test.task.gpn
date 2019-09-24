using Newtonsoft.Json;
using System;

namespace Oil.Api.ViewModels
{
    public class WellTypeViewModel
    {
        public class WellTypeView
        {
            [JsonProperty("id")]
            public Int64 Id { get; set; }

            [JsonProperty("name")]
            public String Name { get; set; }
        }
    }
}
