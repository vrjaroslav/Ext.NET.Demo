using Newtonsoft.Json;
using System.Collections.Generic;

namespace Uber.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class ProductType : BaseItem
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("products")]
        public virtual ICollection<Product> Products { get; set; }
    }
}