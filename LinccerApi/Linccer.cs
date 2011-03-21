
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;


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

        public ClientConfig Config { get; set; }


        public void OnGpsChanged (Double lat, Double lon, int acc)
        {
            Console.WriteLine ("OnGpsChanged");
            
            
            
            Environment.gps = new LocationInfo { latitude = lat, longitude = lon, accuracy = acc, timestamp = Config.TimeNow };
            
            using (var client = new WebClient ()) {
                
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = Config.ClientUri + "/environment";
                client.UploadData (Sign (uri), "PUT", enc.GetBytes (Environment.ToString ()));
            }
        }


        public void Share (String mode, Object payload)
        {
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer ();
            string sJSON = oSerializer.Serialize (payload);
            
            Console.WriteLine (sJSON);
            using (var client = new WebClient ()) {
                
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = Config.ClientUri + "/action/one-to-many";
                client.UploadData (Sign (uri), "PUT", enc.GetBytes (sJSON));
            }
        }

        public T Receive<T> () where T : new()
        {
            
            
            using (var client = new WebClient ()) {

                
                string uri = Config.ClientUri + "/action/one-to-many";
                string json = client.DownloadString (Sign (uri));
                if (json == null || json == "")
                    return default(T);
                
                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer ();

                Console.WriteLine ("Received: " + json);
                return oSerializer.Deserialize<T[]> (json)[0];
            }
            
            
        }

        private string Sign (string uri)
        {
            
            uri += "?api_key=" + Config.ApiKey;
            //uri += "&timestamp=" + Config.TimeNow;
            HMACSHA1 hasher = new HMACSHA1 (Encoding.ASCII.GetBytes (Config.SharedSecret));
            byte[] signature = hasher.ComputeHash (Encoding.ASCII.GetBytes (uri));
            
            return uri + "&signature=" + Convert.ToBase64String (signature);
        }
    }
}
