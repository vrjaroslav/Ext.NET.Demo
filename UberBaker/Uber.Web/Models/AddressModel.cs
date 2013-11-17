using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber.Web.Models
{
    public class AddressModel : BaseModel
    {
        [Required]
        public string StreetAddress { get; set; }

        [Required]
        public string City { get; set; }

        public string State { get; set; }

        public virtual int? CountryId { get; set; }

        public virtual CountryModel Country { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }
}