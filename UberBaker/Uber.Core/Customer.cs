using Newtonsoft.Json;
using System.Collections.Generic;

namespace Uber.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Customer : BaseItem
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        public string FullName { get { return this.FirstName + " " + this.LastName; } }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("orders")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
