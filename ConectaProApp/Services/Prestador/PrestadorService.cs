using ConectaProApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConectaProApp.Services.Prestador
{
    public class PrestadorService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";
        HttpClient client = new HttpClient();

        public PrestadorService()
        {
            _request = new Request();
        }

        public async Task<PrestadorResponseDTO> PostRegistrarPrestadorAsync(PrestadorCreateDTO p)
        {
            string urlComplementar = "/prestador/registro";
            var prestadorRegistrado = await _request
                .PostAsyncFlex<PrestadorCreateDTO, PrestadorResponseDTO>(apiUrlBase + urlComplementar, p, string.Empty);

            return prestadorRegistrado;
        }

        // Buscar prestador pelo nome ou segmento
        public async Task<List<Models.Prestador>> BuscarPrestadorAsync(string termo)
        {
            try
            {
                var token = Preferences.Get("UsuarioToken", string.Empty);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string buscaServicoEndpoint = "/prestador/";
                termo = Uri.EscapeDataString(termo);

                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?termo={termo}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Models.Prestador>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar prestador: " + ex.Message);
            }

            return new List<Models.Prestador>();
        }
    }
}
