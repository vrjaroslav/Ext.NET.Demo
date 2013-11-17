using System;

namespace Uber.Web.Models
{
    public class UserModel : BaseModel
    {
        public string UserName { get; set; }

        public int? RoleId { get; set; }

        public string RoleName { get; set; }

        public string LastLoginIp { get; set; }

        public DateTime? LastLoginDate { get; set; }
    }
}