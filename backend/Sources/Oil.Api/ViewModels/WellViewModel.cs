using Newtonsoft.Json;
using System;
using static Oil.Api.ViewModels.CompanyViewModel;
using static Oil.Api.ViewModels.FieldViewModel;
using static Oil.Api.ViewModels.ShopViewModel;
using static Oil.Api.ViewModels.WellTypeViewModel;

namespace Oil.Api.ViewModels
{
    public class WellViewModel
    {
        public class WellView
        {
            [JsonProperty("id")]
            public Int64 Id { get; set; }

            [JsonProperty("name")]
            public String Name { get; set; }

            [JsonProperty("companyId")]
            public Int64 CompanyId { get; set; }

            [JsonProperty("company")]
            public CompanyView Company { get; set; }

            [JsonProperty("shopId")]
            public Int64 ShopId { get; set; }

            [JsonProperty("shop")]
            public ShopView Shop { get; set; }

            [JsonProperty("fieldId")]
            public Int64 FieldId { get; set; }

            [JsonProperty("field")]
            public FieldView Field { get; set; }

            [JsonProperty("wellTypeId")]
            public Int64 WellTypeId { get; set; }

            [JsonProperty("wellType")]
            public WellTypeView WellType { get; set; }

            [JsonProperty("altitude")]
            public Int64 Altitude { get; set; }

            [JsonProperty("zabI")]
            public Int64 ZabI { get; set; }

            [JsonProperty("zabF")]
            public Int64 ZabF { get; set; }

            [JsonProperty("fieldC")]
            public String FieldC { get; set; }

            [JsonProperty("shopC")]
            public String ShopC { get; set; }

        }
    }
}
