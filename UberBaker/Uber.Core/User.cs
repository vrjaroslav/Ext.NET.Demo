using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber.Core
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public virtual int Id { get; set; }

		public string PhantomId { get; set; }

        public bool Disabled { get; set; }

        public virtual bool IsNew
        {
            get
            {
                return this.Id < 1;
            }
        }
		public string UserName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}
