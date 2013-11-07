using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uber.Core
{
	public class User : BaseItem
	{
        public string UserName { get; set; }

        public string Password { get; set; }

        public int? RoleId { get; set; }

        public Role Role { get; set; }

        public string LastLoginIp { get; set; }

        public DateTime? LastLoginDate { get; set; }
	}
}
