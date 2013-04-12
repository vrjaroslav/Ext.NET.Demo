using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uber.Core;

namespace Uber.Data
{
    public class ProductTypeJsonConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ProductType).IsAssignableFrom(objectType);
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (reader.TokenType == Newtonsoft.Json.JsonToken.Integer)
            {
                return new UberContext().ProductTypes.Find(reader.Value);
            }

            ProductType type = new ProductType();
            serializer.Populate(reader, type);
            return type;
        }

        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, Newtonsoft.Json.JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(((ProductType)value).Id);
            }
            else
            {
                writer.WriteNull();
            }
        }
    }
}
