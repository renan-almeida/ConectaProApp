using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ConectaProApp.Models;
using Newtonsoft.Json;
using ConectaProApp.Models.Enuns;

namespace ConectaProApp.Services.Cliente
{
    public class PerfilEmpresaClienteService
    {
        private readonly ApiService _apiService;
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";
        HttpClient client = new HttpClient();

        public PerfilEmpresaClienteService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public PerfilEmpresaClienteService()
        {
        }

        public async Task<List<SolicitacaoDTO>> BuscarSolicitacoesDaEmpresaAsync(int idEmpresa)
        {
            try
            {
                var endpoint = $"/perfil/empresaCliente/{idEmpresa}/solicitacoes"; // Ajuste conforme o seu endpoint real
                var response = await _apiService.GetAsync<List<SolicitacaoDTO>>(endpoint);

                if (response == null || response.Count == 0)
                    throw new Exception("Nenhuma solicitação publicada");

                return response;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Erro ao buscar solicitações da empresa");
            }
        }

        public async Task<List<ServicoDTO>> BuscarPropostasAsync(int idEmpresa)
        {
            try
            {
                var endpoint = $"/perfil/empresaCliente/{idEmpresa}/propostas"; // Ajuste conforme o seu endpoint real
                var response = await _apiService.GetAsync<List<ServicoDTO>>(endpoint);

                if (response == null || response.Count == 0)
                    throw new Exception("Nenhuma proposta recebida");

                return response;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Erro ao buscar propostas");
            }
        }

        public async Task<List<ServicoDTO>> BuscarHistoricoAsync(int idEmpresa)
        {
            
            try
            {
                var token = await SecureStorage.GetAsync("token");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var endpoint = $"/perfil/empresaCliente/{idEmpresa}/historico"; // Ajuste conforme o seu endpoint real
                var response = await _apiService.GetAsync<List<ServicoDTO>>(endpoint);


                if (response == null)
                    throw new Exception("Nenhum serviço finalizado");

                var filtrados = response.Where(s =>
                s.SituacaoServico != StatusServicoEnum.ORCAMENTO ||
                s.SituacaoServico != StatusServicoEnum.RECUSADO
                ).ToList();

                return response;
            }
            catch (HttpRequestException)
            {
                throw new Exception("Erro ao buscar histórico");
            }
        }
    }
}
