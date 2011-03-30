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
using System.Net;
using System.Security.Cryptography;
using Newtonsoft.Json;
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
            string sJSON = JsonConvert.SerializeObject (payload, Formatting.None, Utils.DefaultSerializerSettings);

            using (var client = new WebClient ()) {
                
                client.Headers.Add (HttpRequestHeader.UserAgent, Config.ApplicationName);
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding ();
                string uri = Config.ClientUri + "/action/" + mode;
                client.UploadData (Sign (uri), "PUT", enc.GetBytes (sJSON));
            }
        }

        public T Receive<T> (string mode, string options = "") where T : new()
        {
            using (var client = new WebClient ()) {
                
                client.Headers.Add (HttpRequestHeader.UserAgent, Config.ApplicationName);
                string uri = Config.ClientUri + "/action/" + mode + "?" + options;
                try {
                    string json = client.DownloadString (Sign (uri));
                    if (json == null || json == "")
                        return default(T);

                    return JsonConvert.DeserializeObject<T[]> (json, Utils.DefaultSerializerSettings)[0];
                } catch (WebException e) {
                    return default(T);
                }
            }
        }
    }
}
