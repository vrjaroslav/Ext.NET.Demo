using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Uber.Core
{
    public class ProductType : BaseItem
    {
        [JsonProperty]
        [DataMember]
        public string Name { get; set; }
    }
}