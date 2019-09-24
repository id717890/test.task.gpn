using Newtonsoft.Json;
using System;
using static Oil.Api.ViewModels.CompanyViewModel;

namespace Oil.Api.ViewModels
{
    public class FieldViewModel
    {
        public class FieldView
        {
            [JsonProperty("id")]
            public Int64 Id { get; set; }

            [JsonProperty("name")]
            public String Name { get; set; }

            [JsonProperty("companyId")]
            public Int64 CompanyId { get; set; }

            [JsonProperty("company")]
            public CompanyView Company { get; set; }

            [JsonProperty("fullName")]
            public String FullName { get; set; }
        }
    }
}
