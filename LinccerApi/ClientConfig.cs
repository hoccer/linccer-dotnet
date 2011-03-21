
using System;

namespace LinccerApi
{


    public class ClientConfig
    {

        public ClientConfig ()
        {
            UseSandboxServers ();
            useDemoApiKey();
        }

        public string ClientUri {
            get { return LinccerUri + "/clients/" + ClientId; }
        }

        public string LinccerUri { get; set; }

        public string ClientId {
            get { return "1122ffaa-8c99-49ee-b045-33d737cc50f9"; }
        }

        public string ApiKey { get; set; }
        public string SharedSecret { get; set; }

        public void UseBetaServers ()
        {
            LinccerUri = "https://linccer-beta.hoccer.com/v3";
            
        }
        private void useDemoApiKey ()
        {
            ApiKey = "e101e890ea97012d6b6f00163e001ab0";
            SharedSecret = "JofbFD6w6xtNYdaDgp4KOXf/k/s=";
        }

        public void UseSandboxServers() {
        LinccerUri = "https://linccer-sandbox.hoccer.com/v3";

    }
    }
}
