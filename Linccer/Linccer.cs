
using System;
using System.Net;


namespace LinccerApi
{


    public class Linccer
    {

        public Linccer (String name)
        {
            this.Name = name;
            Environment = new Environment ();
        }

        public String Name { get; set; }
        public LinccerApi.Environment Environment { get; set; }


        public string ClientID {
            get { return "1122ffaa-8c99-49ee-b045-33d737cc50f9"; }
        }

        public void OnGpsChanged (Double lat, Double lon, int acc)
        {
            Console.WriteLine ("OnGpsChanged");

            TimeSpan ts = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0);

            Environment.gps = new LocationInfo { latitude = lat , longitude = lon, accuracy = acc, timestamp = ts.TotalSeconds.ToString()};

            using (var client = new WebClient ()) {
                
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                
                client.UploadData ("http://linccer-beta.hoccer.com/v3/clients/" + ClientID + "/environment", "PUT", enc.GetBytes (Environment.ToString ()));
            }
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
            return new T ();
        }
    }
}
