using System;
using System.IO;
using System.Threading;
using System.Web;
using LinccerApi;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;

namespace HoccerDemo
{
    class HoccerDemo
    {
        public static void Main (string[] args)
        {
            ClientConfig config = new ClientConfig ("C# Hoccer Demo");
            config.UseProductionServers ();

            Linccer linccer = new Linccer ();
            linccer.Config = config;
            linccer.Gps = new LocationInfo { Latitude = 52.5157, Longitude = 13.409, Accuracy = 1000 };
            linccer.SubmitEnvironment ();
            
            FileCache cache = new FileCache ();
            cache.Config = config;
            
            Hoc h1 = new Hoc();
            h1.Sender.ClientId = "keks";
            Data d=new Data{Type = "image"};
            h1.DataList.Add (d);

            DataContractJsonSerializer serializer = new DataContractJsonSerializer (h1.GetType ());
            MemoryStream ms = new MemoryStream ();
            serializer.WriteObject (ms, h1);

            string json = Encoding.Default.GetString (ms.ToArray ());
            Console.WriteLine(json);

            ms.Position = 0;
            Hoc h2 = (Hoc) serializer.ReadObject(ms);
            Console.WriteLine("the hoc: " + h2.DataList[0].Type);

            for (int i = 3; i > 0; i--) {
                System.Console.Write (i + "... ");
                Thread.Sleep (1 * 1000);
            }
            
            if (args.Length > 0) {
                
                linccer.Share ("one-to-many", null);
            } else {
                System.Console.WriteLine ("Waiting for sender");
                Hoc hoc;
                
                hoc = linccer.Receive<Hoc> ("one-to-one");
                
                if (hoc == null)
                    System.Console.WriteLine ("no sender found");
                else
                    System.Console.WriteLine (hoc);
            }
        }
    }
}
