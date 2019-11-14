using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Swan.Formatters;
using System;

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
            //handle disini buat ambil data yang mau dicerak
        }
        public async Task UpdateStatus(int ticket_id, string api_key)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost");
            client.DefaultRequestHeaders.Add("X-Api-Key", api_key);
            client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            var url = "/saloka/ticket/print_proxy/" + ticket_id;
            HttpResponseMessage response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var resp = await response.Content.ReadAsStringAsync();
            var result = Json.Deserialize<PrintResult>(resp);
            GetResult(result);
        }

        private void GetResult(PrintResult result)
        {
            //handle disini hasil response dari odoo
        }
    }
    
}
