
using System;

namespace LinccerApi
{


    public class Utils
    {


        public static int TimeNow {
            get {
                TimeSpan ts = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0);
                return (int)ts.TotalSeconds;
            }
        }
    }
}
