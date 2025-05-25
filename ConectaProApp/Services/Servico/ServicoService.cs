using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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

        // Tela de busca de prestador
        public async Task<List<Models.Servico>> BuscarServicoAsync(string termo)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string buscaServicoEndpoint = "/servicos/buscar";
                termo = Uri.EscapeDataString(termo);

                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?termo={termo}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Models.Servico>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar serviço: " + ex.Message);
            }

            return new List<Models.Servico>();
        }

        public async Task<List<Models.Servico>> BuscarServicoPorCategoriaAsync(string categoria)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string buscaServicoEndpoint = "/servicos/buscar";
                categoria = Uri.EscapeDataString(categoria);

                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?categoria={categoria}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Models.Servico>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar serviço: " + ex.Message);
            }

            return new List<Models.Servico>();
        }

        // Exibe ao prestador apenas servicos com base na sua UF
        public async Task<List<ServicoHomeDTO>> BuscarServicoUfAsync(string uf)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string urlComplementar = "/Servicos";
                uf = Uri.EscapeDataString(uf);

                var response = await client.GetAsync($"{apiUrlBase}{urlComplementar}?uf={uf}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<ServicoHomeDTO>>(json, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar por UF: " + ex.Message);
            }

            return new List<ServicoHomeDTO>();
        }

        // Exibe Empresa apenas prestadores correspondentes a sua UF

        public async Task<List<Models.Prestador>> BuscarPrestadorUfAsync(string uf)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string urlComplementar = "/Prestador/Uf";
                uf = Uri.EscapeDataString(uf);
                var response = await client.GetAsync($"{apiUrlBase}{urlComplementar}?uf={uf}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Models.Prestador>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar por UF: " + ex.Message);
            }

            return new List<Models.Prestador>();
        }

                

    public async Task<ServicoCreateDTO> PostRegistrarServicoAsync(ServicoCreateDTO s)
                    {
            var token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string urlComplementar = "/solicitacao/registro";

            var servicoRegistrado = await PostAsyncFlex<ServicoCreateDTO, ServicoCreateDTO>(
                apiUrlBase + urlComplementar, s, string.Empty);

            return servicoRegistrado;
        }
    }
}
