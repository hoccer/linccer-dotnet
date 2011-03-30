using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using LinccerApi;


namespace HoccerDemo
{
    public class Hoc
    {
        public Hoc ()
        {
        }

        public Device Sender { get; set; }

        [JsonProperty("data")]
        public List<HocData> DataList { get; set; }

        public override string ToString ()
        {
            return JsonConvert.SerializeObject (this, Formatting.None, Utils.DefaultSerializerSettings);
        }

    }

    public class Device
    {
        [JsonProperty("client-id")]
        public string ClientId { get; set; }

        public string Name { get; set; }
        
    }

    public class HocData
    {
        public HocData ()
        {
        }

        public string Type { get; set; }

        public string Content { get; set; }

        public string Uri { get; set; }
    }
    
    
}

