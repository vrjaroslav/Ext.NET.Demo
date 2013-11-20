using System;
using System.ComponentModel.DataAnnotations;

namespace Uber.Web.Models
{
    public class OrderModel : BaseModel
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

        public virtual ProductModel Product { get; set; }

        public int CustomerId { get; set; }

        public CustomerModel Customer { get; set; }
    }
}