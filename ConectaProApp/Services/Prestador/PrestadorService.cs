using ConectaProApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

using System.Threading.Tasks;
using System.Net.Http;

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
        public async Task<List<PrestadorResponseBuscaDTO>> BuscarPrestadorAsync(string termo)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                
                const string buscaServicoEndpoint = "/busca-prestadores";
                termo = Uri.EscapeDataString(termo);
                
                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?termo={termo}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<PrestadorResponseBuscaDTO>>(json);
                    ;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar prestador: " + ex.Message);
            }

            return new List<PrestadorResponseBuscaDTO>();
        }

    public async Task<List<PrestadorResponseBuscaDTO>> BuscarPrestadorPorCategoriaAsync(string categoria)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string buscaServicoEndpoint = "/busca-prestadores";
                categoria = Uri.EscapeDataString(categoria);

                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?{categoria}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<PrestadorResponseBuscaDTO>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar prestador: " + ex.Message);
            }

            return new List<PrestadorResponseBuscaDTO>();
        }

        public async Task<Models.Prestador> BuscarPrestadorPorIdAsync(int id)
        {

            var token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 

            const string buscaServicoEndpoint = "/prestador/";
            var resposta = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}{id}");

            if (resposta.IsSuccessStatusCode)
            {
                var json = await resposta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Models.Prestador>(json);
            }

            return null;
        }

        public async Task<PropostaCreateDTO> EnviarCandidaturaAsync(PropostaCreateDTO proposta, int idSolicitacao)
        {
            var token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            const string url = $"/solicitacao/solicitacoes/";

            // Serializando com Newtonsoft.Json
            var json = JsonConvert.SerializeObject(proposta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{apiUrlBase}{url}{idSolicitacao}/candidatar", content);

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao enviar proposta: {erro}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            // Desserializando com Newtonsoft.Json
            return JsonConvert.DeserializeObject<PropostaCreateDTO>(responseBody);
        }

    }


}
