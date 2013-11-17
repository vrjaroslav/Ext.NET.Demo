using System.ComponentModel.DataAnnotations;

namespace Uber.Web.Models
{
    public class CountryModel : BaseModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ShortCode { get; set; }
    }
}