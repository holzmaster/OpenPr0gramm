﻿using Newtonsoft.Json;
using System;

namespace OpenPr0gramm.Json
{
    internal class UnixDateTimeConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(DateTime);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            long t = reader.Value is long ? (long)reader.Value : long.Parse((string)reader.Value);
            return DateTimeEx.FromUnixToLocalTime(t);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (!(value is DateTime))
                throw new ArgumentException("Expected date object value.");
            long ticks = ((DateTime)value).ToUnixTime();
            writer.WriteValue(ticks);
        }
    }
}
