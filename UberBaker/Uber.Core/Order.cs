using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber.Core
{
    public class Order : BaseItem
	{
		#region Properties

        public DateTime OrderDate { get; set; }

        public int Quantity { get; set; }

		public double GrossTotal
		{
			get
			{
				return Product == null ? 0 : Product.UnitPrice * Quantity;
			}
		}

		[ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

		public virtual int? ProductId { get; set; }

		#endregion
	}
}