using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Uber.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Product : BaseItem
    {
        //[JsonProperty("name")]
        [JsonProperty]
        public string Name { get; set; }

        //[JsonProperty("description")]
        [DefaultValue("")]
        [JsonProperty]
        public string Description { get; set; }

        //[JsonProperty("unitPrice")]
        [DefaultValue(0.0)]
        [JsonProperty]
        public double UnitPrice { get; set; }

        //[JsonProperty("type")]
        [JsonProperty]
        public virtual ProductType Type { get; set; }
    }
}
