using System.ComponentModel.DataAnnotations.Schema;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Uber.Core
{
	[Model(Name = "ProductType", ClientIdProperty = "PhantomId")]
    public class ProductType : BaseItem
    {
		#region Properties

        [JsonProperty]
		[PresenceValidation]
        public string Name { get; set; }

        [JsonProperty]
		[PresenceValidation]
		[Field(FieldLabel = "Short Code")]
        public string ShortCode { get; set; }

		#endregion
	}
}