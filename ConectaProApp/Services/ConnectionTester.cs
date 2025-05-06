using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ConectaProApp.Services
{
    public class ConnectionTester
    {
        private const string ApiBaseUrl = "https://conectapro-api.azurewebsites.net";

        public async Task<bool> TestarConexaoAsync()
        {
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri(ApiBaseUrl);

                // Endpoint de teste (pode ser um "ping" ou algo similar no back-end)
                var response = await client.GetAsync("/prestador");

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao testar conexão: {ex.Message}");
                return false;
            }
        }
    }
}
