using System;
using System.IO;

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
        
    }
}
