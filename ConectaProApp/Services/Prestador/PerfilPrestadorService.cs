using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using System.Net.Http.Headers;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Globalization;
using ConectaProApp.Converters;

namespace ConectaProApp.Services.Prestador
{
    public class PerfilPrestadorService
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";
        private static readonly HttpClient client = new HttpClient();
        private readonly ApiService _apiService;

        public PerfilPrestadorService()
        {
            _request = new Request();
            _apiService = new ApiService();
        }

        public async Task<List<SolicitacaoDTO>> BuscarPropostasPrestadorAsync(int idPrestador)
        {
            try
            {
                // Pega o idPrestador diretamente do Preferences
               

                // Continua o restante normalmente
                var token = await SecureStorage.GetAsync("token");
                Debug.WriteLine($"🔵 Token obtido: {token}");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var endpoint = $"/perfil/prestador/{idPrestador}/propostas-recebidas";
                var response = await client.GetAsync(apiUrlBase + endpoint);
                Debug.WriteLine("resposta: " + response);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Json: " + json);

                    var propostas = JsonConvert.DeserializeObject<List<SolicitacaoDTO>>(json, new JsonSerializerSettings
                    {
                        Converters = new List<JsonConverter>
                        {new DateTimeFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                     new DateTimeNullableFromStringNewtonsoftConverter("dd/MM/yyyy - HH:mm"),
                     new DateOnlyFromStringNewtonsoftConverter("dd/MM/yyyy"),
                     new DecimalFromStringNewtonsoftConverter(),
                     new SafeStringEnumConverter<FormaPagtoEnum>(),       // seu enum de pagamento
                     new SafeStringEnumConverter<NvlUrgenciaEnum>(),       // seu enum de urgência
                     new SafeStringEnumConverter<TipoSegmentoEnum>(),     // (se tiver outros enums, pode adicionar aqui também)
                     new SafeStringEnumConverter<StatusServicoEnum>()
                        },
                        Culture = new CultureInfo("pt-BR"),

                         Error = (sender, args) =>
                         {
                             args.ErrorContext.Handled = true;
                         }
                    });
                    Debug.WriteLine("propostas: " + propostas);

                    var filtrados = propostas.Where(p =>
                       p.StatusSolicitacao != StatusOrcamentoEnum.FINALIZADA &&
                       p.StatusSolicitacao != StatusOrcamentoEnum.PENDENTE &&
                       p.StatusSolicitacao != StatusOrcamentoEnum.ACEITA &&
                       p.StatusSolicitacao != StatusOrcamentoEnum.RECUSADA
                   ).ToList();

                    return propostas;
                }

                throw new Exception("Erro ao buscar propostas: resposta não foi bem-sucedida.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar propostas: {ex.Message}", ex);
            }
        }
    }
}
