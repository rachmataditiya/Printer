using System;
using EmbedIO;
using EmbedIO.WebApi;
using Swan;
using Swan.Logging;

namespace PrinterConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:9696/";
            if (args.Length > 0)
                url = args[0];

            using (var server = StartWebServer(url))
            {
                server.RunAsync();
                var browser = new System.Diagnostics.Process()
                {
                    StartInfo = new System.Diagnostics.ProcessStartInfo(url) { UseShellExecute = true }
                };
                Console.ReadKey(true);
            }
        }
        private static WebServer StartWebServer(string url)
        {
            var server = new WebServer(o => o
                    .WithUrlPrefix(url)
                    .WithMode(HttpListenerMode.EmbedIO))
                    .WithWebApi("/api", m => m
                    .WithController<DataController>());

            server.StateChanged += (s, e) => $"WebServer New State - {e.NewState}".Info();
            return server;
        }
    }
}
