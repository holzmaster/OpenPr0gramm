using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OpenPr0gramm.Model;

namespace OpenPr0gramm.Json
{
    internal class SyncLogItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(IEnumerable<SyncLogItem>);
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var items = new List<SyncLogItem>();

            if (reader.Value is string value && !string.IsNullOrEmpty(value))
            {
                var bytes = Convert.FromBase64String(value);
                if (bytes.Length % 5 != 0)
                {
                    throw new ArgumentException("Log length out of range");
                }

                var actions = bytes.Length / 5;

                for (var i = 0; i < actions; i++)
                {
                    var id = BitConverter.ToInt32(bytes, i * 5);
                    var action = bytes[i * 5 + 4];

                    items.Add(new SyncLogItem
                    {
                        Action = (SyncAction) action,
                        Id = id
                    });
                }
            }

            return items.ToArray();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var log = new List<byte>();
            if (value is IEnumerable<SyncLogItem> items)
            {
                foreach (var item in items)
                {
                    log.AddRange(BitConverter.GetBytes(item.Id));
                    log.Add((byte)item.Action);
                }
            }

            writer.WriteValue(Convert.ToBase64String(log.ToArray()));
        }
    }
}
