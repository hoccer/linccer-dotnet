using System;
using LinccerApi;

namespace MessageDemo
{

    class Data
    {
        public Data ()
        {
        }

        public string Message { get; set; }
        public int Timestamp {
            get {
                TimeSpan ts = DateTime.UtcNow - new DateTime (1970, 1, 1, 0, 0, 0);

                return (int) ts.TotalSeconds;
            }
        }

    }

    class MessageDemo
    {
        public static void Main (string[] args)
        {
            Linccer linccer = new Linccer ("Demo App");
            linccer.Config = new ClientConfig ();
            //linccer.Config.UseBetaServers();
            linccer.OnGpsChanged (52.5157, 13.409, 1000);
            

            if (args.Length > 0) {
                linccer.Share ("one-to-one", new Data { Message = args[0] });
            } else {
                Data receivedMessage = linccer.Receive<Data> ("one-to-one");
                if (receivedMessage == null)
                    System.Console.WriteLine ("Nothing received");
                else
                    System.Console.WriteLine (receivedMessage.Message);
            }
        }
    }
}
