using System.ComponentModel.DataAnnotations;

namespace Uber.Core
{
    public class ProductType : BaseItem
    {
		[Required]
        public string Name { get; set; }

		[Required]
        public string ShortCode { get; set; }
	}
}