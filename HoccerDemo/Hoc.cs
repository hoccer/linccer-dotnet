using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HoccerDemo
{
    [DataContract]
    public class Hoc
    {
        public Hoc ()
        {
            Sender = new Device ();
            //DataList = new List<HocData> ();
        }

        [DataMember(Name = "sender")]
        public Device Sender { get; set; }

        [DataMember(Name = "data")]
        public HocData DataList { get; set; }

    }

    [DataContract]
    public class Device
    {
        [DataMember(Name = "client-id")]
        public string ClientId { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

    }

    [DataContract]
    public class HocData
    {
        public HocData ()
        {
        }
        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "content")]
        public string Content { get; set; }

        [DataMember(Name = "uri")]
        public string Uri { get; set; }
    }


}

