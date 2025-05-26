using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectaProApp.Models;
using System.Text.Json;
using ConectaProApp.Models.Enuns;


namespace ConectaProApp.Services.Solicitacao
{
    public class SolicitacaoService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiService _apiService;

        public SolicitacaoService(HttpClient httpClient, ApiService apiService)
        {
            _httpClient = httpClient;
            _apiService = apiService;
            _httpClient.BaseAddress = new Uri("https://conectapro-api.azurewebsites.net");
        }

        public async Task<SolicitacaoDTO> CriarSolicitacaoAsync(SolicitacaoCreateDTO dto)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/solicitacoes", content);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao criar solicitação");

            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<SolicitacaoDTO>(responseBody);
        }

        public async Task<List<SolicitacaoDTO>> BuscarSolicitacoesPorClienteAsync(int idCliente)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync($"/solicitacoes/cliente/{idCliente}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao buscar solicitações do cliente");

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<SolicitacaoDTO>>(json);
        }

        public async Task<List<SolicitacaoDTO>> BuscarSolicitacoesPorPrestadorAsync(int idPrestador)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();
            var response = await _httpClient.GetAsync($"/solicitacoes/prestador/{idPrestador}");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao buscar solicitações do prestador");

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<SolicitacaoDTO>>(json);
        }

        public async Task AtualizarStatusSolicitacaoAsync(int idSolicitacao, int idServico, StatusOrcamentoEnum novoStatus)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();

            var content = new StringContent(JsonSerializer.Serialize(new
            {
                IdSolicitacao = idSolicitacao,
                IdServico = idServico,
                Status = novoStatus.ToString()
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/solicitacoes/{idSolicitacao}/status", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao atualizar status da solicitação");

            // 🔄 Remover propostas não aceitas se a solicitação for aprovada
            if (novoStatus == StatusOrcamentoEnum.ACEITA)
            {
                await RemoverPropostasNaoAceitasAsync(idSolicitacao);
            }
        }

        private async Task RemoverPropostasNaoAceitasAsync(int idSolicitacao)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync($"/solicitacoes/{idSolicitacao}/propostas");

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao buscar propostas relacionadas");

            var json = await response.Content.ReadAsStringAsync();
            var propostas = JsonSerializer.Deserialize<List<SolicitacaoDTO>>(json);

            foreach (var proposta in propostas)
            {
                if (proposta.StatusOrcamentoEnum == StatusOrcamentoEnum.PENDENTE)
                {
                    await RemoverSolicitacaoAsync(proposta.IdSolicitacao);
                }
            }
        }

        public async Task EnviarPropostaAsync(int idSolicitacao, SolicitacaoDTO proposta)
        {
            await _apiService.ConfigureAuthorizationHeaderAsync();

            var json = JsonSerializer.Serialize(new
            {
                IdSolicitacao = idSolicitacao,
                IdPrestador = proposta.IdPrestador,
                Descricao = proposta.Descricao,
                ValorProposto = proposta.ValorProposto
            });

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"/solicitacoes/{idSolicitacao}/propostas", content);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao enviar proposta");
        }


        public async Task<bool> RemoverSolicitacaoAsync(int id)
        {
            return await _apiService.DeleteAsync($"/solicitacoes/{id}");
        }
    }
}