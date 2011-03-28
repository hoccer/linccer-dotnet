using System;
using System.IO;
using LinccerApi;

namespace HoccerDemo
{
    class MainClass
    {
        public static void Main (string[] args)
        {
            ClientConfig config = new ClientConfig ("C# Hoccer Demo");
            config.UseSandboxServers ();

            Linccer linccer = new Linccer ();
            linccer.Config = config;
            linccer.Gps = new LocationInfo { Latitude = 52.5157, Longitude = 13.409, Accuracy = 1000 };
            linccer.SubmitEnvironment ();

            FileCache cache = new FileCache();
            cache.Config = config;
            FileStream data = File.OpenRead("/home/mamta/Desktop/Screenshot.png");
            string uri = cache.Store(data,300);

            Console.WriteLine(uri);
        }
    }
}
