using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber.Core
{
	public class Address : BaseItem
	{
		[Required]
		public string StreetAddress { get; set; }

		[Required]
		public string City { get; set; }

		public string State { get; set; }

		public virtual int? CountryId { get; set; }

		[ForeignKey("CountryId")]
		[DefaultValue(null)]
		public virtual Country Country { get; set; }

		[Required]
		public string ZipCode { get; set; }
	}
}
