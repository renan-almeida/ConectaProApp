using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using System.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Globalization;
using ConectaProApp.Converters;


namespace ConectaProApp.Services.Cliente
{
    public class PerfilEmpresaClienteService
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";
        private static readonly HttpClient client = new HttpClient();
        private readonly ApiService _apiService;

       

        // Injete ApiService via construtor obrigatório
        public PerfilEmpresaClienteService()
        {
            
            _request = new Request();
            _apiService = new ApiService();
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
                var token = await SecureStorage.GetAsync("token");
                Debug.WriteLine($"🔵 Token obtido: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var endpoint = $"/perfil/empresaCliente/{idEmpresa}/historico";
                var response = await client.GetAsync(apiUrlBase + endpoint);
                System.Diagnostics.Debug.WriteLine("resposta: " + response); // Logando a URL para diagnóstico

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Json: " + json);

                    var servicos = JsonConvert.DeserializeObject<List<ServicoDTO>>(json, new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter>
                        {
                            new DateTimeFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                            new DateTimeNullableFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                            new DateOnlyFromStringNewtonsoftConverter("dd/MM/yyyy"),
                            new DecimalFromStringNewtonsoftConverter()
                        },
                        Culture = new CultureInfo("pt-BR")
                    });
                    Debug.WriteLine("servicos: " + servicos);

                    // Filtrando os serviços válidos
                    var filtrados = servicos.Where(s =>
                        s.SituacaoServico != StatusServicoEnum.ORCAMENTO &&
                        s.SituacaoServico != StatusServicoEnum.RECUSADO
                    ).ToList();

                    return filtrados;
                }

                throw new Exception("Erro ao buscar histórico: resposta não foi bem-sucedida.");



            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar histórico: {ex.Message}", ex);
            }
        }
    }
}
