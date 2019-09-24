using Newtonsoft.Json;
using System;

namespace Oil.Api.ViewModels
{
    public class DeleteViewModel
    {
        [JsonProperty("id")]
        public Int64 Id { get; set; }
    }
}
