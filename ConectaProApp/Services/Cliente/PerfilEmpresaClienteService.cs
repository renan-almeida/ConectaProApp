using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConectaProApp.Models;
using ConectaProApp.Services;
using ConectaProApp.Models.Enuns;

namespace ConectaProApp.Services.Cliente
{
    public class PerfilEmpresaClienteService
    {
        private readonly ApiService _apiService;

        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";

        // Injete ApiService via construtor obrigatório
        public PerfilEmpresaClienteService(ApiService apiService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
        }

        public async Task<List<SolicitacaoDTO>> BuscarSolicitacoesDaEmpresaAsync(int idEmpresa)
        {
            try
            {
                var endpoint = $"/perfil/empresaCliente/{idEmpresa}/solicitacoes";
                var response = await _apiService.GetAsync<List<SolicitacaoDTO>>(endpoint);

                if (response == null || response.Count == 0)
                    throw new Exception("Nenhuma solicitação publicada");

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar solicitações da empresa: {ex.Message}", ex);
            }
        }

        public async Task<List<ServicoDTO>> BuscarPropostasAsync(int idEmpresa)
        {
            try
            {
                var endpoint = $"/perfil/empresaCliente/{idEmpresa}/propostas";
                var response = await _apiService.GetAsync<List<ServicoDTO>>(endpoint);

                if (response == null || response.Count == 0)
                    throw new Exception("Nenhuma proposta recebida");

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar propostas: {ex.Message}", ex);
            }
        }

        public async Task<List<ServicoDTO>> BuscarHistoricoAsync(int idEmpresa)
        {
            try
            {
                var endpoint = $"/perfil/empresaCliente/{idEmpresa}/historico";
                var response = await _apiService.GetAsync<List<ServicoDTO>>(endpoint);

                if (response == null || response.Count == 0)
                    throw new Exception("Nenhum serviço finalizado");

                // Filtra conforme sua regra, ajustei para usar '&&' ao invés de '||'
                var filtrados = response.Where(s =>
                    s.SituacaoServico != StatusServicoEnum.ORCAMENTO &&
                    s.SituacaoServico != StatusServicoEnum.RECUSADO
                ).ToList();

                return filtrados;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar histórico: {ex.Message}", ex);
            }
        }
    }
}
