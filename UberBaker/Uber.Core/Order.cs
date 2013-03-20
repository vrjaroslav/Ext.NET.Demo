using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Uber.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Order : BaseItem
    {
        [DefaultValue(null)]
        [JsonProperty("customer")]
        public Customer Customer { get; set; }

        [DefaultValue(null)]
        [JsonProperty("deliveryDate")]
        public DateTime? DeliveryDate { get; set; }

        [DefaultValue(0.0)]
        [JsonProperty("totalPrice")]
        public double TotalPrice { get; set; }

        [DefaultValue(null)]
        [JsonProperty("orderItems")]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
