using ConectaProApp.Converters;
using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;


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
                var token = await SecureStorage.GetAsync("token");
                Debug.WriteLine($"🔵 Token obtido: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var endpoint = $"/perfil/empresaCliente/{idEmpresa}/solicitacoes";
                var response = await client.GetAsync(apiUrlBase + endpoint);
                Debug.WriteLine("resposta: " + response);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Json: " + json);

                    var solicitacoes = JsonConvert.DeserializeObject<List<SolicitacaoDTO>>(json, new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter>
                {
                    new DateTimeFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                    new DateTimeNullableFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                    new DateOnlyFromStringNewtonsoftConverter("dd/MM/yyyy"),
                    new DecimalFromStringNewtonsoftConverter(),
                    new SafeStringEnumConverter<FormaPagtoEnum>(),
                    new SafeStringEnumConverter<NvlUrgenciaEnum>(),
                    new SafeStringEnumConverter<TipoSegmentoEnum>(),
                    new SafeStringEnumConverter<StatusOrcamentoEnum>()
                },
                        Culture = new CultureInfo("pt-BR"),

                        


                    });

                    Debug.WriteLine("Solicitacoes desserializadas: " + solicitacoes?.Count);

                    // Filtrando as solicitações ativas (mantendo sua lógica limpa como no histórico)
                    var filtrados = solicitacoes
                        .Where(s => s.StatusSolicitacao == StatusOrcamentoEnum.ATIVA
                                 )
                        .ToList();

                    return filtrados;
                }

                throw new Exception("Erro ao buscar solicitações: resposta não foi bem-sucedida.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar solicitações: {ex.Message}", ex);
            }
        }


        public async Task<List<ServicoDTO>> BuscarPropostasAsync(int idEmpresa)
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                Debug.WriteLine($"🔵 Token obtido: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var endpoint = $"/perfil/empresaCliente/{idEmpresa}/propostas";
                var response = await client.GetAsync(apiUrlBase + endpoint);
                Debug.WriteLine("resposta: " + response);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Json: " + json);

                    var settings = new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter>
    {
                     new DateTimeFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                     new DateTimeNullableFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                     new DateOnlyFromStringNewtonsoftConverter("dd/MM/yyyy"),
                     new DecimalFromStringNewtonsoftConverter(),
                     new SafeStringEnumConverter<FormaPagtoEnum>(),       // seu enum de pagamento
                     new SafeStringEnumConverter<NvlUrgenciaEnum>(),       // seu enum de urgência
                     new SafeStringEnumConverter<TipoSegmentoEnum>(),     // (se tiver outros enums, pode adicionar aqui também)
                     new SafeStringEnumConverter<StatusServicoEnum>()      // (exemplo)
    },
                        Culture = new CultureInfo("pt-BR"),

                        // Esse trecho aqui vai ignorar qualquer erro de deserialização:
                        Error = (sender, args) =>
                        {
                            args.ErrorContext.Handled = true;
                        }
                    };
                    var servicos = JsonConvert.DeserializeObject<List<ServicoDTO>>(json, settings);
                    Debug.WriteLine("servicos: " + servicos);

                    var filtrados = servicos.Where(s =>
                        s.SituacaoServico == StatusServicoEnum.ORCAMENTO
                    ).ToList();

                    return filtrados;
                }

                throw new Exception("Erro ao buscar propostas: resposta não foi bem-sucedida.");
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
