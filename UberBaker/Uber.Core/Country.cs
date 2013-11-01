using System.ComponentModel.DataAnnotations;

namespace Uber.Core
{
	public class Country : BaseItem
	{
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortCode { get; set; }
	}
}
