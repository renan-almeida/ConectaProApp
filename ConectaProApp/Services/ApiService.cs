using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConectaProApp.Services
{


    // Fazer login e guardar o token de autenticação. Fazer chamadas GET autenticadas com esse token. Centralizar toda a comunicação segura com a API.




    public class ApiService
    {
        private static readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://conectapro-api.azurewebsites.net")
        };

        public async Task<string> LoginAsync(string email, string senha)
        {
            var loginData = new { email, senha };
            var content = new StringContent(JsonSerializer.Serialize(loginData), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/login", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var token = JsonSerializer.Deserialize<TokenResponse>(responseContent)?.Token;

                if (!string.IsNullOrEmpty(token))
                {
                    await SecureStorage.SetAsync("jwt_token", token);
                    await ConfigureAuthorizationHeaderAsync();
                    return token;
                }
            }

            throw new Exception($"Erro ao fazer login: {response.ReasonPhrase}");
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            await ConfigureAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content);
            }

            throw new Exception($"Erro ao buscar dados: {response.ReasonPhrase}");
        }

        public async Task ConfigureAuthorizationHeaderAsync()
        {
            var token = await SecureStorage.GetAsync("jwt_token");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            var response = await _httpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
