using System.Linq;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber.Core
{
	[Model(Name = "Order", ClientIdProperty = "PhantomId")]
	[HasManyAssociation(Model = "OrderItem", AssociationKey = "OrderItems", Name = "orderItems")]
    public class Order : BaseItem
	{
		#region Properties

		[JsonProperty]
        public DateTime OrderDate { get; set; }

		[JsonProperty]
		[Field(ReadOnly = true)]
		public double GrossTotal
		{
			get
			{
				return OrderItems == null ? 0 : OrderItems.Sum(item => item.UnitPrice * item.Quantity);
			}
		}

        [JsonProperty]
        public virtual ICollection<OrderItem> OrderItems { get; set; }

		#endregion
	}
}