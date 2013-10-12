using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Serialization;

namespace Uber.Core
{
	[Model(Name = "OrderItem", ClientIdProperty = "PhantomId")]
    public class OrderItem : BaseItem
    {
		#region Properties

        [JsonProperty]
		[ForeignKey("ProductId")]
        public Product Product { get; set; }

		[Field(Ignore = true)]
		public int? ProductId { get; set; }

        [JsonProperty]
		[PresenceValidation]
        public double UnitPrice { get; set; }

        [JsonProperty]
		[PresenceValidation]
        public int Quantity { get; set; }

		#endregion
	}
}