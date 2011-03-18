
using System;

namespace LinccerApi
{


    public class Linccer
    {
        public String Name {get;set;}

        public Linccer (String name)
        {
            this.Name = name;
        }
        
        public void OnGpsChanged (Double latitude, Double longitude, int accuracy)
        {
            Console.WriteLine ("OnGpsChanged");
        }
        
        public void Share (String mode)
        {
            Console.WriteLine ("share");
        }

        public void Share (String mode, Object payload)
        {
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer ();
            string sJSON = oSerializer.Serialize (payload);

            Console.WriteLine (sJSON);
        }

        public T Receive<T> () where T : new()
        {
            Console.WriteLine ("Receive");
            return new T();
        }
    }
}
