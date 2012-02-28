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
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Collections.Generic;

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
            HMACSHA1 hasher = new HMACSHA1 (Encoding.UTF8.GetBytes (Config.SharedSecret));
            byte[] signature = hasher.ComputeHash (Encoding.UTF8.GetBytes (uri));
            
            return uri + "&signature=" + HttpUtility.UrlEncode (Convert.ToBase64String (signature));
        }

		public List<KeyValuePair<string, string>> SignatureParameters(string uri)
		{
			HMACSHA1 hasher = new HMACSHA1(Encoding.UTF8.GetBytes(Config.SharedSecret));
			byte[] signature = hasher.ComputeHash(Encoding.UTF8.GetBytes(uri));

			return new List<KeyValuePair<string, string>>
			{
				new KeyValuePair<string, string>("api_key", Config.ApiKey),
				new KeyValuePair<string, string>("timestamp", Utils.TimeNow.ToString()),
				new KeyValuePair<string, string>("signature", HttpUtility.UrlEncode(Convert.ToBase64String(signature)))
			};

		}
    }
}

