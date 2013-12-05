using System.Collections.Generic;

namespace Uber.Web.Models
{
    public class RoleModel : BaseModel
    {
        public string Name { get; set; }

        public virtual List<PermissionModel> Permisions { get; set; }
    }
}