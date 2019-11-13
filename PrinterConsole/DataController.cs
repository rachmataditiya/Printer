using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;

namespace PrinterConsole
{
    public sealed class DataController : WebApiController
    {
        [Route(HttpVerbs.Post, "/print")]
        public async Task Ticket()
        {
            var data = await HttpContext.GetRequestDataAsync<Ticket>();
            Console.WriteLine("Test");
        }
    }
}
