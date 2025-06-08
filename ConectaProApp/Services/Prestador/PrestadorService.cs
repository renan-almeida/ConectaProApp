using ConectaProApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

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

                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?termo={categoria}");

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

        public async Task<PrestadorResponseBuscaDTO?> BuscarPrestadorPorIdAsync(int id)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string buscaServicoEndpoint = "/prestador/";
                var resposta = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}{id}");
                Debug.WriteLine("Resposta da API: " + resposta);

                if (resposta.IsSuccessStatusCode)
                {
                    var json = await resposta.Content.ReadAsStringAsync();
                    Debug.WriteLine($"[DEBUG] JSON recebido: {json}");
                    if (!string.IsNullOrWhiteSpace(json))
                    {
                        var prestador = JsonConvert.DeserializeObject<PrestadorResponseBuscaDTO>(json);
                        return prestador;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"[ERRO] BuscarPrestadorPorIdAsync: {ex.Message}");
            }

            return null;
        }


        public async Task<PropostaCreateDTO> EnviarCandidaturaAsync(PropostaCreateDTO proposta, int idSolicitacao)
        {
            var token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            const string url = $"/solicitacao/solicitacoes/";
            const string urlfinal = "/candidatar";

            // Serializando com Newtonsoft.Json
            var json = JsonConvert.SerializeObject(proposta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await PostAsyncFlexToken<PropostaCreateDTO, PropostaCreateDTO>(apiUrlBase + url + idSolicitacao + urlfinal, proposta, token);
            // Opcionalmente, serialize para visualizar
            Debug.WriteLine("Resposta da API: " + JsonConvert.SerializeObject(response));

            return response;
        }

    }


}
