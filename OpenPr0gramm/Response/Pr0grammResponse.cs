using Newtonsoft.Json;
using OpenPr0gramm.Json;
using System;

namespace OpenPr0gramm
{
    public class Pr0grammResponse
    {
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TS { get; set; }
        public object Cache { get; set; }
        public int RT { get; set; }
        public int QC { get; set; }
    }
}
