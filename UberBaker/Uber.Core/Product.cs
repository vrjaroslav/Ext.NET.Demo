using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ext.Net;
using Ext.Net.MVC;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;

namespace Uber.Core
{
	[Model(Name = "Product", ClientIdProperty = "PhantomId")]
    public class Product : BaseItem
	{
		#region Properties

		[JsonProperty]
		[PresenceValidation]
        public string Name { get; set; }

        [JsonProperty]
		[PresenceValidation]
		[Field(FieldLabel = "Short Code")]
        public string ShortCode { get; set; }

        [DefaultValue("")]
        [JsonProperty]
		[Field(FieldType = typeof(TextArea))]
        public string Description { get; set; }

        [DefaultValue(0.0)]
        [JsonProperty]
        public double UnitPrice { get; set; }

		[JsonProperty]        
		[ModelField(Name = "ProductType", ServerMapping = "ProductType.Name", Mapping = "ProductType.Name")]
		[ForeignKey("ProductTypeId")]
		[DefaultValue(null)]
		public virtual ProductType ProductType { get; set; }

		[Field(Ignore = true)]
		[ModelField(Name = "ProductTypeId", ServerMapping = "ProductType.Id", Mapping = "ProductType.Id", NullConvert = true)]
		public virtual int? ProductTypeId { get; set; }

		#endregion
	}
}