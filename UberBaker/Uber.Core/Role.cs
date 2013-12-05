using System.Collections.Generic;

namespace Uber.Core
{
    [Securable]
    public class Role : BaseItem
    {
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Permission> Permisions { get; set; } 
    }
}
