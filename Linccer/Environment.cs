
using System;

namespace LinccerApi
{


    public class Environment
    {


        public Environment ()
        {
        }

        public LocationInfo gps { get; set; }
        public LocationInfo network { get; set; }

        override public string ToString(){

             System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer ();
            string json = oSerializer.Serialize (this);

            return json;
        }
    }
}
