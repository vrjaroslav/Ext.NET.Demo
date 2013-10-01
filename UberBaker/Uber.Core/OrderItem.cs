using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber.Core
{
    public class OrderItem : BaseItem
    {
        [JsonProperty]
        public Product Product { get; set; }

        [JsonProperty]
        public double UnitPrice { get; set; }

        [JsonProperty]
        public int Quantity { get; set; }
    }
}