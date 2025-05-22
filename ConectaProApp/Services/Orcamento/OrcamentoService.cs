using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace ConectaProApp.Services.Orcamento
{
    public class OrcamentoService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiService _apiService;

        public OrcamentoService(HttpClient httpClient, ApiService apiService)
        {
            _httpClient = httpClient;
            _apiService = apiService;
            _httpClient.BaseAddress = new Uri("https://conectapro-api.azurewebsites.net");
        }

        public async Task<OrcamentoDTO> CriarOrcamentoAsync(OrcamentoCreateDTO dto)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/orcamentos", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao criar orçamento");

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OrcamentoDTO>(responseBody);
        }

        public async Task<List<OrcamentoDTO>> BuscarOrcamentosPorClienteAsync(int idCliente)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync($"/orcamentos/cliente/{idCliente}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao buscar orçamentos do cliente");

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<OrcamentoDTO>>(json);
        }

        public async Task<List<OrcamentoDTO>> BuscarOrcamentosPorPrestadorAsync(int idPrestador)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync($"/orcamentos/prestador/{idPrestador}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao buscar orçamentos do prestador");

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<OrcamentoDTO>>(json);
        }

        public async Task AtualizarStatusOrcamentoAsync(int idOrcamento, StatusOrcamentoEnum novoStatus)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();

            var content = new StringContent(JsonSerializer.Serialize(new
            {
                status = novoStatus.ToString()
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/orcamentos/{idOrcamento}/status", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao atualizar status do orçamento");
        }

        public async Task EnviarPropostaAsync(OrcamentoDTO proposta)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();

            var json = JsonSerializer.Serialize(proposta);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/orcamentos/{proposta.Id}", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao enviar proposta");
        }
    }
}
