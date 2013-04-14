using Newtonsoft.Json;
using System;
using Uber.Core;

namespace Uber.Data
{
    public class ProductTypeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(ProductType).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Integer)
            {
                return new UberContext().ProductTypes.Find(reader.Value);
            }

            ProductType type = new ProductType();
            serializer.Populate(reader, type);

            return type;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
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
