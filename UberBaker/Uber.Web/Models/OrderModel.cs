using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Uber.Core;

namespace Uber.Web.Models
{
    public class OrderModel : BaseModel
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int Quantity { get; set; }

        public double GrossTotal
        {
            get
            {
                return Product == null ? 0 : Product.UnitPrice * Quantity;
            }
        }

        public virtual int? ProductId { get; set; }

        public virtual ProductModel Product { get; set; }

        public int CustomerId { get; set; }

        public CustomerModel Customer { get; set; }
    }
}