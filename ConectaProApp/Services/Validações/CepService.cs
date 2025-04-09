using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Services.Validações
{
    public class CepService
    {
        private const string baseUrl = "https://viacep.com.br/ws/";

        public async Task<bool> ValidarCepAsync(string cep)
        {
            try
            {
                using var client = new HttpClient();
                var response = await client.GetAsync($"{baseUrl}{cep}/json/");

                if (!response.IsSuccessStatusCode) return false;

                var json = await response.Content.ReadAsStringAsync();
                return !json.Contains("\"erro\": true");
            }
            catch
            {
                return false;
            }
        }
    }
}
