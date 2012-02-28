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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace LinccerApi
{


    public static class Utils
    {
        public static int TimeNow {
            get {
                TimeSpan ts = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0);
                return (int)ts.TotalSeconds;
            }
        }

        public static void CopyTo (this Stream src, Stream dest)
        {
            int size = (src.CanSeek) ? Math.Min ((int)(src.Length - src.Position), 0x2000) : 0x2000;
            byte[] buffer = new byte[size];
            int n;
            do {
                n = src.Read (buffer, 0, buffer.Length);
                dest.Write (buffer, 0, n);
            } while (n != 0);
        }

        public static JsonSerializerSettings DefaultSerializerSettings {

            get {
                JsonSerializerSettings settings = new JsonSerializerSettings ();
                settings.Error = delegate(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs args) {
                    Console.WriteLine("error: " + sender);
                };
                
                settings.ContractResolver = new CamelCasePropertyNamesContractResolver ();
                settings.DefaultValueHandling = DefaultValueHandling.Ignore;
                
                return settings;
            }
        }

		public static byte[] StringToAscii(string s)
		{
			byte[] retval = new byte[s.Length];
			for (int ix = 0; ix < s.Length; ++ix)
			{
				char ch = s[ix];
				if (ch <= 0x7f) retval[ix] = (byte)ch;
				else retval[ix] = (byte)'?';
			}
			return retval;
		}
    }
}
