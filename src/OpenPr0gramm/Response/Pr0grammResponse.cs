using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using OpenPr0gramm.Json;

using System;
using System.Collections.Generic;

namespace OpenPr0gramm
{
    public class Pr0grammResponse
    {
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TS { get; private set; }
        public object Cache { get; private set; }
        public int RT { get; private set; }
        public int QC { get; private set; }
        
        [JsonExtensionData]
        public IDictionary<string, JToken> ExtraData { get; private set; }
    }
}
