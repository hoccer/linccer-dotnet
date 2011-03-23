
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;




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

        public LocationInfo Gps {
            set { Environment.Gps = value; }
        }
        public LocationInfo Network {
            set { Environment.Network = value; }
        }

        public void SubmitEnvironment ()
        {
            using (var client = new WebClient ()) {
                
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = Config.ClientUri + "/environment";
                client.UploadData (Sign (uri), "PUT", enc.GetBytes (Environment.ToString ()));
            }
        }


        public void Share (String mode, Object payload, String filename)
        {
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer ();
            string sJSON = oSerializer.Serialize (payload);
            
            using (var client = new WebClient ()) {
                
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = Config.ClientUri + "/action/" + mode;
                client.UploadData (Sign (uri), "PUT", enc.GetBytes (sJSON));
                client.UploadFile (Sign (uri), filename);
            }
        }

        public T Receive<T> (string mode, string options, string filename) where T : new()
        {
            using (var client = new WebClient ()) {
                string uri = Config.ClientUri + "/action/" + mode + "?" + options;
                //try {
                client.DownloadFile (Sign (uri), filename);
                string json = client.DownloadString (Sign (uri));
                if (json == null || json == "")
                    return default(T);
                
                System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer ();
                return oSerializer.Deserialize<T[]> (json)[0];
                //}catch(WebException e){
                //  return default(T);
                //}
            }
            
        }

        private string Sign (string uri)
        {
            uri += uri.Contains ("?") ? "&" : "?";
            uri += "api_key=" + Config.ApiKey;
            uri += "&timestamp=" + Utils.TimeNow;
            HMACSHA1 hasher = new HMACSHA1 (Encoding.ASCII.GetBytes (Config.SharedSecret));
            byte[] signature = hasher.ComputeHash (Encoding.ASCII.GetBytes (uri));
            
            return uri + "&signature=" + HttpUtility.UrlEncode (Convert.ToBase64String (signature));
        }
    }
}
