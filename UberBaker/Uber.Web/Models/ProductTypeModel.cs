using System.ComponentModel.DataAnnotations;
using Ext.Net.MVC;

namespace Uber.Web.Models
{
    public class ProductTypeModel : BaseModel
    {
        [Required]
        [Column(Width = 150, Order = 2)]
        public string Name { get; set; }

        [Required]
        [Column(Width = 100, Order = 3)]
        [Field(FieldLabel = "Short Code")]
        public string ShortCode { get; set; }
    }
}