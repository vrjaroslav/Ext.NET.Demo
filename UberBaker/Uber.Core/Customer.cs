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

        /// TODO: Need to separate into an Address object.
        /// Need BillingAddress and ShippingAddress properties
		[Required]
		public string StreetAddress { get; set; }

		[Required]
		public string City { get; set; }

		public string State { get; set; }

		[Required]
		public string Country { get; set; }

		[Required]
		public string ZipCode { get; set; }

		[Required]
		public string ContactPhone { get; set; }

		public string CellPhone { get; set; }

		public string Fax { get; set; }
	}
}
