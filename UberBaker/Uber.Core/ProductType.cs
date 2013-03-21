using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Uber.Core
{
    [JsonObject(MemberSerialization.OptIn)]
    [DataContract]
    public class ProductType : BaseItem
    {
        [JsonProperty("name")]
        [DataMember]
        public string Name { get; set; }
    }
}