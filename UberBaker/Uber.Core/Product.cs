using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Uber.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Product : BaseItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [DefaultValue("")]
        [JsonProperty("description")]
        public string Description { get; set; }

        [DefaultValue(0.0)]
        [JsonProperty("unitPrice")]
        public double UnitPrice { get; set; }

        [JsonProperty("type")]
        public virtual ProductType Type { get; set; }
    }
}
