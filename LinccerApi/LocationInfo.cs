
using System;
using System.Runtime.Serialization;

namespace LinccerApi
{


[DataContract]
    public class LocationInfo
    {

        public LocationInfo ()
        {
            Timestamp = Utils.TimeNow;
        }

        [DataMember(Name = "latitude")]
        public double Latitude { get; set; }
        [DataMember(Name = "longitude")]
        public double Longitude { get; set; }
        [DataMember(Name = "accuracy")]
        public int Accuracy { get; set; }
        [DataMember(Name = "timestamp")]
        public int Timestamp { get; set; }

        
        
    }
}
