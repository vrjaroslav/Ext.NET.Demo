using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Uber.Core
{
    public class Product : BaseItem
	{
		#region Properties

        public string Name { get; set; }

        public string ShortCode { get; set; }

        [DefaultValue("")]
        public string Description { get; set; }

        [DefaultValue(0.0)]
        public double UnitPrice { get; set; }

		[ForeignKey("ProductTypeId")]
		[DefaultValue(null)]
		public virtual ProductType ProductType { get; set; }

		public virtual int? ProductTypeId { get; set; }

		#endregion
	}
}