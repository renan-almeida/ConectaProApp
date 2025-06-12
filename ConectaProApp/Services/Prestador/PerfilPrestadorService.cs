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

        public async Task<List<ServicoDTO>> BuscarPropostasPrestadorAsync(int idPrestador)
        {
            try
            {
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

                    var propostas = JsonConvert.DeserializeObject<List<ServicoDTO>>(json, new JsonSerializerSettings
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
                    Debug.WriteLine("propostas: " + propostas);

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
