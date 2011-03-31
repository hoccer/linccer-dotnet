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
            Linccer linccer = new Linccer ();
            linccer.Config = new ClientConfig ("C# Message Demo");
            linccer.Config.UseBetaServers ();
            linccer.Gps = new LocationInfo { Latitude = 52.5157, Longitude = 13.409, Accuracy = 1000 };
            linccer.SubmitEnvironment ();
            
            
            if (args.Length > 0) {
                for (int i = 3; i > 0; i--) {
                    System.Console.Write (i + "... ");
                    Thread.Sleep (1 * 1000);
                }
                linccer.Share ("one-to-many", new Data { Message = args[0] });
            } else {
                System.Console.WriteLine ("Waiting for message");
                Data receivedMessage;
                do {
                    receivedMessage = linccer.Receive<Data> ("one-to-many", "waiting=true");
                } while (receivedMessage == null);
                
                System.Console.WriteLine (receivedMessage.Message);
            }
        }
    }
}
