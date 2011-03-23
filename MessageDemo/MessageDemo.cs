using System;
using System.Threading;
using LinccerApi;

namespace MessageDemo
{

    class Data
    {
        public Data ()
        {
        }

        public string Message { get; set; }

    }

    class MessageDemo
    {
        public static void Main (string[] args)
        {
            Linccer linccer = new Linccer ("Demo App");
            linccer.Config = new ClientConfig ();
            linccer.Config.UseProductionServers();
            linccer.Gps = new LocationInfo { Latitude = 52.5157, Longitude = 13.409, Accuracy = 1000 };
            linccer.Network = new LocationInfo { Latitude = 52.5157, Longitude = 13.409, Accuracy = 1000 };
            linccer.SubmitEnvironment();

            for (int i = 3; i > 0; i--) {
                System.Console.Write (i + "... ");
                Thread.Sleep (1 * 1000);
            }
            
            if (args.Length > 0) {
                linccer.Share ("one-to-many", new Data { Message = args[0] });
            } else {
                Data receivedMessage = linccer.Receive<Data> ("one-to-many","waiting=true");
                if (receivedMessage == null)
                    System.Console.WriteLine ("Nothing received");
                else
                    System.Console.WriteLine (receivedMessage.Message);
            }
        }
    }
}
