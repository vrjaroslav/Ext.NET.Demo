namespace Uber.Core
{
    public class Permission : BaseItem
    {
        public string ObjectType { get; set; }

        public PermissionType PermissionType { get; set; }

        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }   
    }
}
