using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Services.Validações
{
    public class CnpjService
    {
        private const string baseUrl = "https://www.receitaws.com.br/v1/cnpj/";

        public async Task<bool> ValidarCnpjAsync(string cnpj)
        {
            try
            {
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("User-Agent", "ConectaProApp");

                var response = await client.GetAsync($"{baseUrl}{cnpj}");
                if (!response.IsSuccessStatusCode) return false;

                var json = await response.Content.ReadAsStringAsync();
                return !json.Contains("\"status\":\"ERROR\"");
            }
            catch
            {
                return false;
            }
        }
    }
}
