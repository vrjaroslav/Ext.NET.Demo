using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Uber.Core
{
    [Securable]
    public class Product : BaseItem
	{
		[Required]
        public string Name { get; set; }

		[Required]
        public string ShortCode { get; set; }

        [DefaultValue("")]
        public string Description { get; set; }

        [DefaultValue(0.0)]
        public decimal UnitPrice { get; set; }

		public virtual int? ProductTypeId { get; set; }

		[ForeignKey("ProductTypeId")]
		[DefaultValue(null)]
		public virtual ProductType ProductType { get; set; }
	}
}