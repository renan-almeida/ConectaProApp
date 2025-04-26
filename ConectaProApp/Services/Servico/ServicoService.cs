using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ConectaProApp.Models;

namespace ConectaProApp.Services.Servico
{
    public class ServicoService: Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";
        private static readonly HttpClient client = new HttpClient();

        public ServicoService()
        {
            _request = new Request();
        }

        public async Task<List<Models.Servico>> BuscarServicoAsync(string termo)
        {
              const string buscaServicoEndpoint = "/servicos/buscar";
               termo = Uri.EscapeDataString(termo);
            var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?termo={termo}");
            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Models.Servico>>(json);
            }
            return new List<Models.Servico>();
        }
    }
}
