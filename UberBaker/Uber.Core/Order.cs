﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber.Core
{
    [Securable]
    public class Order : BaseItem
	{
		[Required]
        public DateTime OrderDate { get; set; }

		[Required]
        public int Quantity { get; set; }

		public decimal GrossTotal
		{
			get
			{
				return Product == null ? 0 : Product.UnitPrice * Quantity;
			}
		}

		public virtual int? ProductId { get; set; }

		[ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

		public int CustomerId { get; set; }

		[ForeignKey("CustomerId")]
		public Customer Customer { get; set; }
	}
}