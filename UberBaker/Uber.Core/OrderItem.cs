using Newtonsoft.Json;
using System.ComponentModel;

namespace Uber.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OrderItem : BaseItem
    {
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [DefaultValue(null)]
        [JsonProperty("product")]
        public virtual Product Product { get; set; }

        [DefaultValue(null)]
        [JsonProperty("order")]
        public virtual Order Order { get; set; }
    }
}
