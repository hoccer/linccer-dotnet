
using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace LinccerApi
{

    [DataContract]
    public class Environment
    {


        public Environment ()
        {
        }

        [DataMember(Name = "gps")]
        public LocationInfo Gps { get; set; }

        //[DataMember(Name = "network")]
      //  public LocationInfo Network { get; set; }

        public override string ToString ()
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer (this.GetType ());
            MemoryStream ms = new MemoryStream ();
            serializer.WriteObject (ms, this);
            string json = Encoding.Default.GetString (ms.ToArray ());
            
            return json;
        }
    }
}
