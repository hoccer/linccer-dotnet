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
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Text;
using RestSharp;
using System.Linq;
using System.Diagnostics;

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

        public void SubmitEnvironment (LinccerContentCallback callback)
        {
			string uri = Config.ClientUri + "/environment";
			var signedUri = Sign(uri);
			var client = new RestClient
			{
				BaseUrl = Sign(uri)
			};

			var request = new RestRequest
			{
				Method = Method.PUT
			};

			request.AddHeader(HttpRequestHeader.UserAgent.ToString(), Config.ApplicationName);
			request.AddParameter("text/xml", Environment.ToString(), ParameterType.RequestBody);

			Debug.WriteLine(Sign(signedUri));

			client.ExecuteAsync(request, (response, req)=>
			{
				callback(response.Content);
			});
        }

        public void Share (String mode, Object payload, LinccerContentCallback callback)
        {
			string uri = Config.ClientUri + "/action/" + mode;
			var client = new RestClient
			{
				BaseUrl = Sign(uri)
			};

			var request = new RestRequest
			{
				Method = Method.PUT
			};

			string sJSON = JsonConvert.SerializeObject(payload, Formatting.None, Utils.DefaultSerializerSettings);
			request.AddHeader(HttpRequestHeader.UserAgent.ToString(), Config.ApplicationName);
			request.AddParameter("text/xml", sJSON, ParameterType.RequestBody);

			client.ExecuteAsync(request, (response, req) =>
			{
				callback(response.Content);
			});

        }

        public void Receive<T> (string mode, LinccerReceiveCallback<T> callback, string options = "") where T : new()
        {
			string uri = Config.ClientUri + "/action/" + mode + "?" + options;
			var client = new RestClient
			{
				BaseUrl = Sign(uri)
			};

			var request = new RestRequest
			{
				Method = Method.GET
			};

			request.AddHeader(HttpRequestHeader.UserAgent.ToString(), Config.ApplicationName);

			client.ExecuteAsync(request, (response, req) =>
			{
				var json = response.Content;
				if (json == null || json == "")
					callback(default(T));

				callback(JsonConvert.DeserializeObject<List<T>>(json, Utils.DefaultSerializerSettings)[0]);
			});
        }
    }
}
