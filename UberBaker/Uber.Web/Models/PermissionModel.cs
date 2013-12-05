using Uber.Core;

namespace Uber.Web.Models
{
    public class PermissionModel : BaseModel
    {
        public string ObjectType { get; set; }

        public PermissionType PermissionType { get; set; }
    }
}