using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Uber.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Product : BaseItem
    {
        [JsonProperty]
        public string Name { get; set; }

        [DefaultValue("")]
        [JsonProperty]
        public string Description { get; set; }

        [DefaultValue(0.0)]
        [JsonProperty]
        public double UnitPrice { get; set; }

        [JsonProperty]
        public virtual ProductType Type { get; set; }
    }
}
