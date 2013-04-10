using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Uber.Core
{
    public class Order : BaseItem
    {
        [JsonProperty]
        public DateTime OrderDate { get; set; }

        [JsonProperty]
        public double GrossTotal { get; set; }

        [JsonProperty]
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}