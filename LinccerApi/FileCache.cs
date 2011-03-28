using System;
using System.Net;
using System.IO;

namespace LinccerApi
{


    public class FileCache : CloudService
    {
        public FileCache ()
        {
        }


        public string Store (Stream data, int secondsUntilExipred)
        {
            using (var client = new WebClient ()) {
                client.Headers.Add (HttpRequestHeader.UserAgent, Config.ApplicationName);
                string uri = Config.FileCacheUri + "/" + Guid.NewGuid ().ToString ();
                
                Stream sink = client.OpenWrite (uri + "/?expires_in=" + secondsUntilExipred, "PUT");
                data.CopyTo(sink);
                sink.Close();

                return uri;
            }
            
        }

        public void Fetch (string uri, string fileName)
        {
            
            using (var client = new WebClient ()) {
                client.Headers.Add (HttpRequestHeader.UserAgent, Config.ApplicationName);
                client.DownloadFile (uri, fileName);
            }
        }
    }
}

