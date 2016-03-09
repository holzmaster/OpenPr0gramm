using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
    public class Pr0grammResponse
    {
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TS { get; private set; }
        public object Cache { get; private set; }
        public int RT { get; private set; }
        public int QC { get; private set; }
    }
}
