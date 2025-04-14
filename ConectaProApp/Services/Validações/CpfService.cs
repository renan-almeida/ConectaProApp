using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConectaProApp.Services.Validações
{
    public class CpfService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiToken;

        public CpfService(string apiToken)
        {
            _apiToken = apiToken;
            _httpClient = new HttpClient();
        }

        public async Task<bool> ValidarCpfAsync(string cpf)
        {
            try
            {
                var UrlBase = $"https://api.invertexto.com/v1/validator?value={cpf}&type=cpf";

                var requisicao = new HttpRequestMessage(HttpMethod.Get, UrlBase);
                requisicao.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _apiToken);

                var response = await _httpClient.SendAsync(requisicao);

                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                
                var content = await response.Content.ReadAsStringAsync();
                var resultado = JsonSerializer.Deserialize<InvertextoResponse>(content);

                return resultado.Valid;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao validar CPF: {ex.Message}");
                return false;

            }
        }
        public class InvertextoResponse
        {
            public bool Valid { get; set; }
        }
    }
}
