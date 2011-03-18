
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
            
            Environment.gps = new LocationInfo { latitude = lat, longitude = lon, accuracy = acc, timestamp = (int)ts.TotalSeconds };
            
            using (var client = new WebClient ()) {
                
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = "http://linccer-beta.hoccer.com/v3/clients/" + ClientID + "/environment";
                client.UploadData (Sign (uri), "PUT", enc.GetBytes (Environment.ToString ()));
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
            using (var client = new WebClient ()) {
                
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = "http://linccer-beta.hoccer.com/v3/clients/" + ClientID + "/action/one-to-many";
                client.UploadData (Sign (uri), "PUT", enc.GetBytes (sJSON));
            }
        }

        public T Receive<T> () where T : new()
        {
            
            
            using (var client = new WebClient ()) {
                
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = "http://linccer-beta.hoccer.com/v3/clients/" + ClientID + "/action/one-to-many";
                string json = client.DownloadString (Sign (uri));
                if(json == null)
                    return default(T);

                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer ();

                Console.WriteLine ("Received: " + json);
                return oSerializer.Deserialize<T[]> (json)[0];
            }
            

        }

        private string Sign (string uri)
        {
            uri += "?api_key=e101e890ea97012d6b6f00163e001ab0";
            return uri;
        }
    }
}
