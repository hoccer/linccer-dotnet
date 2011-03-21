
using System;

namespace LinccerApi
{


    public class LocationInfo
    {

        public LocationInfo ()
        {
            timestamp = Utils.TimeNow;
        }

        public double latitude { get; set; }
        public double longitude { get; set; }
        public int accuracy { get; set; }
        public int timestamp { get; set; }

        
        
    }
}
