using System.ComponentModel.DataAnnotations;

namespace Uber.Web.Models
{
    public class ProductModel : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortCode { get; set; }

        public string Description { get; set; }

        public double UnitPrice { get; set; }

        public virtual int? ProductTypeId { get; set; }

        public virtual ProductTypeModel ProductType { get; set; }
    }
}