using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConectaProApp.Models;
using Newtonsoft.Json;
using ServicoModel = ConectaProApp.Models.Servico;

namespace ConectaProApp.Services.Servico
{
    public class ServicoService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";
        private static readonly HttpClient client = new HttpClient();

        public ServicoService()
        {
            _request = new Request();
        }

        private readonly ApiService _apiService;

        public ServicoService(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Tela de busca de prestador
        public async Task<List<ServicoModel>> BuscarServicoAsync(string termo)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string buscaServicoEndpoint = "/busca-solicitacoes/";
                termo = Uri.EscapeDataString(termo);

                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?termo={termo}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServicoModel>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar serviço: " + ex.Message);
            }

            return new List<ServicoModel>();
        }

        public async Task<List<ServicoModel>> BuscarServicoPorCategoriaAsync(string categoria)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string buscaServicoEndpoint = "/solicitacoes-busca";

                //busca-solicitacoes/uf=${}&termo=${}

                categoria = Uri.EscapeDataString(categoria);

                var response = await client.GetAsync($"{apiUrlBase}{buscaServicoEndpoint}?{categoria}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServicoModel>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar serviço: " + ex.Message);
            }

            return new List<ServicoModel>();
        }

        // Exibe ao prestador apenas servicos com base na sua UF
        public async Task<List<ServicoHomeDTO>> BuscarServicoUfAsync(string uf)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string urlComplementar = "/solicitacoes-busca";
                uf = Uri.EscapeDataString(uf);

                var response = await client.GetAsync($"{apiUrlBase}{urlComplementar}?uf={uf}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServicoHomeDTO>>(json);
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
                var token = Preferences.Get("token", string.Empty);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                const string urlComplementar = "/Prestador/Uf";
                uf = Uri.EscapeDataString(uf);
                var response = await client.GetAsync($"{apiUrlBase}{urlComplementar}?uf={uf}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Models.Prestador>>(json);
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

            var servicoRegistrado = await PostAsyncFlexToken<ServicoCreateDTO, ServicoCreateDTO>(
                apiUrlBase + urlComplementar, s, token);

            return servicoRegistrado;
        }

        public async Task<ServicoModel> EnviarPropostaAsync(ServicoCreateDTO proposta)
        {
            var token = await SecureStorage.GetAsync("token");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            const string url = "/servicos/propostas";

            // Serializando com Newtonsoft.Json
            var json = JsonConvert.SerializeObject(proposta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync($"{apiUrlBase}{url}", content);

            if (!response.IsSuccessStatusCode)
            {
                var erro = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao enviar proposta: {erro}");
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            // Desserializando com Newtonsoft.Json
            return JsonConvert.DeserializeObject<ServicoModel>(responseBody);
        }


        public async Task AceitarPropostaAsync(int idServicoAceito, int idSolicitacao)
        {
            var token = Preferences.Get("token", string.Empty);

            // 1. Atualizar status da proposta aceita
            var contentAceita = new
            {
                statusOrcamento = "ACEITO"
            };

            var responseAceita = await _request.PutAsync(
                $"{apiUrlBase}/servicos/{idServicoAceito}/status", contentAceita, token);

            if (!responseAceita.IsSuccessStatusCode)
                throw new Exception("Erro ao aceitar proposta");

            // 2. Buscar outras propostas da mesma solicitação
            var propostas = await BuscarPropostasPorSolicitacaoAsync(idSolicitacao);

            foreach (var proposta in propostas.Where(p => p.IdServico != idServicoAceito))
            {
                var contentRecusa = new
                {
                    statusOrcamento = "RECUSADO"
                };

                var responseRecusa = await _request.PutAsync(
                    $"{apiUrlBase}/servicos/{proposta.IdServico}/status", contentRecusa, token);

                if (!responseRecusa.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro ao recusar proposta {proposta.IdServico}");
                }
            }
        }

        public async Task<List<ServicoModel>> BuscarPropostasPorSolicitacaoAsync(int idSolicitacao)
        {
            try
            {
                var token = Preferences.Get("token", string.Empty); // ou SecureStorage.GetAsync("token") se for mais seguro
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await client.GetAsync($"{apiUrlBase}/servicos/propostas?solicitacaoId={idSolicitacao}");

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<ServicoModel>>(json);
                }
                else
                {
                    Console.WriteLine($"Erro ao buscar propostas: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar propostas: " + ex.Message);
            }

            return new List<ServicoModel>();
        }

    }
}
