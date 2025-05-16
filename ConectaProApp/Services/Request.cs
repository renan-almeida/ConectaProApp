using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Services
{
    public class Request
    {
        HttpClient httpClient = new HttpClient();

        public async Task<int> PostReturnIntAsync<TResult>(string uri, TResult data, string token)
        {
            

            httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            string serialized = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return int.Parse(serialized);
            else
                throw new Exception(serialized);
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token)
        {

            httpClient.DefaultRequestHeaders.Authorization
            = new AuthenticationHeaderValue("Bearer", token);

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);
            string serialized = await response.Content.ReadAsStringAsync();
            TResult result = data;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                result = await Task.Run(() => JsonConvert.DeserializeObject<TResult>(serialized));
            else
                throw new Exception(serialized);

            return result;
        }

        public async Task<TRetorno> PostAsyncFlex<TEnvio, TRetorno>(string uri, TEnvio data, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = null; // LIMPA primeiro

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpResponseMessage response = await httpClient.PostAsync(uri, content);
            string serialized = await response.Content.ReadAsStringAsync();


            var jsonBody = JsonConvert.SerializeObject(data);
            Console.WriteLine("🔵 JSON ENVIADO:");
            Console.WriteLine(jsonBody);


            Console.WriteLine("🔴 ERRO DA API:");
            Console.WriteLine(serialized);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<TRetorno>(serialized);
            }
            else
            {
                throw new Exception($"Erro na requisição: {response.StatusCode} - {serialized}");
            }
        }



    }
}
