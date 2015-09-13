using Newtonsoft.Json;
using System;

namespace OpenPr0gramm.Json
{
    internal abstract class RelativeUrlConverter : JsonConverter
    {
        protected abstract string UrlPrefix { get; }
        public override bool CanConvert(Type objectType) => objectType == typeof(string);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            string str = reader.Value is string ? (string)reader.Value : reader.Value.ToString();
            if (string.IsNullOrEmpty(str))
                return str;
            if (str[0] != '/')
                str = "/" + str;
            return UrlPrefix + str;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotSupportedException();
        }
    }
    internal class ThumbnailUrlConverter : RelativeUrlConverter
    {
        protected override string UrlPrefix { get; } = Pr0grammApiClient.ThumbnailUrlPrefix;
    }
    internal class FullUrlConverter : RelativeUrlConverter
    {
        protected override string UrlPrefix { get; } = Pr0grammApiClient.FullUrlPrefix;
    }
    internal class ImageUrlConverter : RelativeUrlConverter
    {
        protected override string UrlPrefix { get; } = Pr0grammApiClient.ImageUrlPrefix;
    }
}
