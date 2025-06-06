using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConectaProApp.Converters;

namespace ConectaProApp.Services
{
    public class ApiService
    {
        public HttpClient HttpClient { get; }

        private readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public ApiService()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri("https://conectapro-api.azurewebsites.net/")
            };

            // Adicionando os conversores personalizados
            _jsonOptions.Converters.Add(new DateTimeFromStringConverter("dd/MM/yyyy - HH:mm"));
            _jsonOptions.Converters.Add(new DateOnlyFromStringConverter("dd/MM/yyyy"));
            _jsonOptions.Converters.Add(new DecimalFromStringConverter());
        }

        public async Task<string> LoginAsync(string email, string senha)
        {
            var loginData = new { email, senha };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync("/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<TokenResponse>(responseContent)?.Token;

                if (!string.IsNullOrEmpty(token))
                {
                    await SecureStorage.SetAsync("jwt_token", token);
                    Debug.WriteLine($"✅ Token salvo no SecureStorage: {token}");
                    await ConfigureAuthorizationHeaderAsync();
                    return token;
                }
            }

            throw new Exception($"Erro ao fazer login: {response.ReasonPhrase}");
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            await ConfigureAuthorizationHeaderAsync();

            var response = await HttpClient.GetAsync(endpoint);
            if (!response.IsSuccessStatusCode)
                throw new Exception($"Erro ao buscar dados: {response.ReasonPhrase}");

            var content = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions();
            options.Converters.Add(new DateOnlyFromStringConverter("dd/MM/yyyy"));
            options.Converters.Add(new DateTimeFromStringConverter("dd/MM/yyyy - HH:mm"));
            options.Converters.Add(new DateTimeNullableFromStringConverter("dd/MM/yyyy - HH:mm"));
            options.Converters.Add(new DecimalFromStringConverter());

            return JsonSerializer.Deserialize<T>(content, options);
        }


        public async Task<bool> DeleteAsync(string endpoint)
        {
            await ConfigureAuthorizationHeaderAsync();
            var response = await HttpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AtualizarFotoPerfilAsync(string endpointApi, string urlFoto)
        {
            await ConfigureAuthorizationHeaderAsync();

            var json = JsonSerializer.Serialize(new { caminhoFoto = urlFoto }, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await HttpClient.PutAsync(endpointApi, content);
            return response.IsSuccessStatusCode;
        }

        public async Task ConfigureAuthorizationHeaderAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");

            if (string.IsNullOrEmpty(token))
            {
                token = Preferences.Get("jwt_token", string.Empty);
                Debug.WriteLine($"🔹 Token recuperado via Preferences: {token}");
            }

            if (!string.IsNullOrEmpty(token))
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Debug.WriteLine($"🔹 Cabeçalho de autorização configurado com token: {token}");
            }
            else
            {
                Debug.WriteLine("⚠️ Token não encontrado!");
            }
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
