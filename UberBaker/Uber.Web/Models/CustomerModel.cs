using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Uber.Core;

namespace Uber.Web.Models
{
    public class CustomerModel : BaseModel
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

        public virtual AddressModel BillingAddress { get; set; }

        public virtual int? ShippingAddressId { get; set; }

        public virtual AddressModel ShippingAddress { get; set; }

        [Required]
        public string ContactPhone { get; set; }

        public string CellPhone { get; set; }

        public string Fax { get; set; }
    }
}