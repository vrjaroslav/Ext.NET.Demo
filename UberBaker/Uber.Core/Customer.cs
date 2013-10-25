using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber.Core
{
	public class Customer : BaseItem
	{
		[Required]
		public string FirstName { get; set; }
		
		[Required]
		public string LastName { get; set; }

		public string FullName
		{
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
		}
	
		[Required]
		public string Company { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		public virtual int? BillingAddressId { get; set; }

		[ForeignKey("BillingAddressId")]
		[DefaultValue(null)]
		public virtual Address BillingAddress { get; set; }

		public virtual int? ShippingAddressId { get; set; }

		[ForeignKey("ShippingAddressId")]
		[DefaultValue(null)]
		public virtual Address ShippingAddress { get; set; }

		[Required]
		public string ContactPhone { get; set; }

		public string CellPhone { get; set; }

		public string Fax { get; set; }
	}
}
