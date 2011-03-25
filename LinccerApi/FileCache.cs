using System;
using System.Net;

namespace LinccerApi
{


    public class FileCache : CloudService
    {
        public FileCache ()
        {
        }


        public void Store (string fileName)
        {
            using (var client = new WebClient ()) {
                client.UploadFile (Config.FileCacheUri, fileName);
            }
        }

        public void Fetch (string uri, string fileName)
        {
            
            using (var client = new WebClient ()) {
                client.DownloadFile (uri, fileName);
            }
        }
    }
}

