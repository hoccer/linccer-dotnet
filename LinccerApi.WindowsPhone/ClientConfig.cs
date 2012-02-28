/*
Copyright (C) 2009, 2010, Hoccer GmbH Berlin, Germany <www.hoccer.com>

These coded instructions, statements, and computer programs contain
proprietary information of Linccer GmbH Berlin, and are copy protected
by law. They may be used, modified and redistributed under the terms
of GNU General Public License referenced below.

Alternative licensing without the obligations of the GPL is
available upon request.

GPL v3 Licensing:

This file is part of the "Linccer .Net-API".

Linccer .Net-API is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

Linccer .Net-API is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with Linccer .Net-API. If not, see <http://www.gnu.org/licenses/>.
 */

using System;

namespace LinccerApi
{


    public class ClientConfig
    {

        public ClientConfig (string applicationName)
        {
            ApplicationName = applicationName;
            ClientId = Guid.NewGuid().ToString();
            UseSandboxServers ();
            useDemoApiKey ();
        }

        public string ClientUri {
            get { return LinccerUri + "/clients/" + ClientId; }
        }

        public string LinccerUriHost { get; set; }

		public string LinccerUri { get; set; }

        public string ClientId { get; set; }

        public string FileCacheUri{get;set;}

		public string FileCacheUriHost { get; set; }

        public string ApiKey { get; set; }

        public string SharedSecret { get; set; }

        public string ApplicationName{get;set;}

		public string Version { get; set; }

        

        public void UseBetaServers ()
        {
            LinccerUri = "http://linccer-beta.hoccer.com/v3";
            FileCacheUri = "http://filecache-beta.hoccer.com/v3";

			LinccerUriHost = "http://linccer.hoccer.com/v3";
			FileCacheUriHost = "http://filecache.hoccer.com/v3";
            
        }
        private void useDemoApiKey ()
        {
            ApiKey = "e101e890ea97012d6b6f00163e001ab0";
            SharedSecret = "JofbFD6w6xtNYdaDgp4KOXf/k/s=";
        }

        public void UseSandboxServers ()
        {
            LinccerUri = "https://linccer-sandbox.hoccer.com/v3";
            FileCacheUri = "https://filecache-sandbox.hoccer.com/v3";

			LinccerUriHost = "https://linccer-sandbox.hoccer.com/v3";
			FileCacheUriHost = "https://filecache-sandbox.hoccer.com/v3";
            
        }

        public void UseProductionServers ()
        {
            LinccerUri = "https://linccer.hoccer.com/v3";
            FileCacheUri = "https://filecache.hoccer.com/v3";

			LinccerUriHost = "https://linccer.hoccer.com/v3";
			FileCacheUriHost = "https://filecache.hoccer.com/v3";

			Version = "v3";

        }
    }
}
