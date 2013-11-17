using System;

namespace Uber.Core
{
    public class UserProfile : BaseItem
    {
        public virtual int? UserId { get; set; }

        public virtual User User { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
} 
