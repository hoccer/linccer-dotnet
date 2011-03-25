using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LinccerApi
{
    public class CloudService
    {
        public CloudService ()
        {
        }

        public ClientConfig Config { get; set; }

        public string Sign (string uri)
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

