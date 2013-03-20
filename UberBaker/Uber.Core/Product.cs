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

        [JsonIgnore]
        public virtual ProductType Type { get; set; }

        [DefaultValue(-1)]
        [JsonProperty("typeId")]
        public int TypeId
        {
            get
            {
                return this.Type != null ? this.Type.Id : -1;
            }
        }

        [JsonProperty("orderItems")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
