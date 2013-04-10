using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Uber.Core
{
    public class ProductType : BaseItem
    {
        [JsonProperty("name")]
        [DataMember]
        public string Name { get; set; }
    }
}