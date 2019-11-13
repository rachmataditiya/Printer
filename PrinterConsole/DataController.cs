using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Swan.Formatters;


namespace PrinterConsole
{
    public sealed class DataController : WebApiController
    {
        [Route(HttpVerbs.Post, "/print")]
        public async Task Ticket()
        {
            var data = await HttpContext.GetRequestDataAsync<List<Ticket>>();
            CetakTicket(data);
        }

        private void CetakTicket(List<Ticket> data)
        {
            string tanggal = data[0].date;
        }
        public async Task UpdateStatus(int ticket_id, string api_key)
        {
            var client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Put, "http://localhost/saloka/ticket/print_proxy/" + ticket_id);
            requestMessage.Headers.Add("X-Api-Key", api_key);
            HttpResponseMessage response = await client.SendAsync(requestMessage);
            var result = Json.Deserialize<PrintResult>(await response.Content.ReadAsStringAsync());
        }

    }
    
}
