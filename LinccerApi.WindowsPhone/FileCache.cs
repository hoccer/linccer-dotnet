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
using System.IO;
using RestSharp;

namespace LinccerApi.WindowsPhone
{


    public class FileCache : CloudService
    {
        public FileCache ()
        {
        }


        public void Store (byte[] data, int secondsUntilExipred, FileCacheStoreCallback callback)
        {
			var guid = Guid.NewGuid().ToString();
			var uri = Config.FileCacheUri + "/" + guid;

			var client = new RestClient
			{
				BaseUrl = uri
			};
			
			var request = new RestRequest
			{
				Method = Method.PUT
			};

			request.AddFile("image/jpeg", data, "file");

			client.UploadRaw = true;

			client.ExecuteAsync(request, (response, req) =>
			{
				callback(uri);
			});
            
        }

        public void Fetch (string uri, FileCacheGetCallback callback)
        {

			var client = new RestClient
			{
				BaseUrl = uri
			};

			var request = new RestRequest
			{
				Method = Method.GET
			};

			request.AddHeader(HttpRequestHeader.UserAgent.ToString(), Config.ApplicationName);

			client.ExecuteAsync(request, (response, req) =>
			{
				callback(response.RawBytes);
			});
        }
    }
}

