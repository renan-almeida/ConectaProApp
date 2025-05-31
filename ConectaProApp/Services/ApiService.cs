using System;
using System.Diagnostics;
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
        
        public HttpClient HttpClient { get; }

        public ApiService()
        {
            HttpClient = new HttpClient
            {
                BaseAddress = new Uri("https://conectapro-api.azurewebsites.net/")
            };
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

            if (string.IsNullOrEmpty(token))
            {
                token = Preferences.Get("jwt_token", string.Empty); // Teste alternativo
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

        public async Task<bool> DeleteAsync(string endpoint)
        {
            await ConfigureAuthorizationHeaderAsync();
            var response = await HttpClient.DeleteAsync(endpoint);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AtualizarFotoPerfilAsync(string endpointApi, string urlFoto)
        {
            await ConfigureAuthorizationHeaderAsync();

            var json = JsonSerializer.Serialize(new { caminhoFoto = urlFoto });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await HttpClient.PutAsync(endpointApi, content);
            return response.IsSuccessStatusCode;
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }
    }
}
