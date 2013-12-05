using System.ComponentModel.DataAnnotations;
using Ext.Net.MVC;

namespace Uber.Web.Models
{
    [Model(ClientIdProperty = "PhantomId", Name = "Product")]
    public class ProductModel : BaseModel
    {
        [Required]
        [Column(Width = 150, Order = 2)]
        public string Name { get; set; }

        [Required]
        [Column(Width = 100, Order = 3)]
        [Field(FieldLabel = "Short Code")]
        public string ShortCode { get; set; }

        [Column(Flex = 1, Order = 4)]
        public string Description { get; set; }

        [ModelField()]
        [Column(Width = 100, Order = 5)]
        public decimal UnitPrice { get; set; }

        [ModelField(NullConvert = true)]
        [Column(Ignore = true)]
        [Field(Ignore = true)]
        public int? ProductTypeId { get; set; }

        // TODO Set Sortable = false
        [Column(Width = 100, Order = 6)]
        [Field(Ignore = true)]
        public string ProductTypeName { get; set; }
    }
}