using System;
using Newtonsoft.Json;

namespace OpenPr0gramm.Json
{
    internal class ImageUrlConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(string);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is string value && !string.IsNullOrEmpty(value))
            {
                var s = !value.StartsWith("/");
                return $"{ClientConstants.GetImageUrlPrefix(true)}{(s ? "/" : "")}{value}";
            }

            return string.Empty;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
