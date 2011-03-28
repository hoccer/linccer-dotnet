
using System;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LinccerApi
{

    
    public class Linccer : CloudService
    {
        public Linccer ()
        {
            Environment = new Environment ();
        }

        public LinccerApi.Environment Environment { get; set; }

        public LocationInfo Gps {
            set { Environment.Gps = value; }
        }

        public LocationInfo Network {
            set { Environment.Network = value; }
        }

        public void SubmitEnvironment ()
        {
            using (var client = new WebClient ()) {

                client.Headers.Add (HttpRequestHeader.UserAgent, Config.ApplicationName);
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = Config.ClientUri + "/environment";
                client.UploadData (Sign (uri), "PUT", enc.GetBytes (Environment.ToString ()));
            }
        }

        public void Share (String mode, Object payload)
        {
            System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer ();
            string sJSON = oSerializer.Serialize (payload);
            
            using (var client = new WebClient ()) {
                
                client.Headers.Add (HttpRequestHeader.UserAgent, Config.ApplicationName);
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = Config.ClientUri + "/action/" + mode;
                client.UploadData (Sign (uri), "PUT", enc.GetBytes (sJSON));
            }
        }

        public T Receive<T> (string mode, string options) where T : new()
        {
            using (var client = new WebClient ()) {
                
                client.Headers.Add (HttpRequestHeader.UserAgent, Config.ApplicationName);
                string uri = Config.ClientUri + "/action/" + mode + "?" + options;
                try {
                    string json = client.DownloadString (Sign (uri));
                    if (json == null || json == "")
                        return default(T);
                    
                    System.Web.Script.Serialization.JavaScriptSerializer oSerializer = new System.Web.Script.Serialization.JavaScriptSerializer ();
                    return oSerializer.Deserialize<T[]> (json)[0];
                } catch (WebException e) {
                    return default(T);
                }
            }
        }
    }
}
