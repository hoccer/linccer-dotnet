
using System;

namespace LinccerApi
{


    public class ClientConfig
    {

        public ClientConfig ()
        {
            UseBetaServers ();
        }
        
        public string ClientUri {
            get { return LinccerUri + "/clients/" + ClientId; }
        }
        public string LinccerUri { get; set; }
        public string ClientId {
            get { return "1122ffaa-8c99-49ee-b045-33d737cc50f9"; }
        }
        public void UseBetaServers ()
        {
            LinccerUri = "http://linccer-beta.hoccer.com/v3";
            
        }
    }
}
