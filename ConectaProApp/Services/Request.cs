using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try
            {
                // Limpa token anterior
                httpClient.DefaultRequestHeaders.Authorization = null;

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
                Debug.WriteLine("🔵 JSON ENVIADO:");
                Debug.WriteLine(jsonBody);

                Debug.WriteLine($"🔴 RESPOSTA DA API: Status: {response.StatusCode}");
                Debug.WriteLine(serialized);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TRetorno>(serialized);
                }
                else
                {
                    string mensagemErro = $"Erro ao chamar a API: StatusCode: {response.StatusCode}";

                    // Tenta extrair uma mensagem legível de erro da API
                    try
                    {
                        var erroApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);
                        if (erroApi != null && erroApi.ContainsKey("mensagem"))
                        {
                            mensagemErro = erroApi["mensagem"];
                        }
                        else
                        {
                            mensagemErro = serialized;
                        }
                    }
                    catch
                    {
                        // Se a resposta não for JSON, exibe o texto puro
                        mensagemErro = serialized;
                    }

                    await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "OK");

                    return default(TRetorno);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exceção inesperada: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erro inesperado", ex.Message, "OK");
                return default(TRetorno);
            }
        }


        public async Task<TRetorno> PostAsyncFlexToken<TEnvio, TRetorno>(string uri, TEnvio data, string token)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                HttpResponseMessage response = await httpClient.PostAsync(uri, content);
                string serialized = await response.Content.ReadAsStringAsync();

                var jsonBody = JsonConvert.SerializeObject(data);
                Debug.WriteLine("🔵 JSON ENVIADO:");
                Debug.WriteLine(jsonBody);

                Debug.WriteLine($"🔴 RESPOSTA DA API: Status: {response.StatusCode}");
                Debug.WriteLine(serialized);

                if (response.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<TRetorno>(serialized);
                }
                else
                {
                    string mensagemErro = $"Erro ao chamar a API: StatusCode: {response.StatusCode}";

                    // Tenta extrair mensagem de erro do JSON (caso a API retorne algo tipo { "mensagem": "erro tal" })
                    try
                    {
                        var erroApi = JsonConvert.DeserializeObject<Dictionary<string, string>>(serialized);
                        if (erroApi != null && erroApi.ContainsKey("mensagem"))
                        {
                            mensagemErro = erroApi["mensagem"];
                        }
                        else
                        {
                            // Caso a API retorne um erro diferente, exibe o corpo bruto
                            mensagemErro = serialized;
                        }
                    }
                    catch
                    {
                        // Caso a resposta não seja JSON
                        mensagemErro = serialized;
                    }

                    // Mostra o erro ao usuário
                    await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "OK");

                    // Retorna valor default (evita o throw)
                    return default(TRetorno);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exceção inesperada: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Erro inesperado", ex.Message, "OK");
                return default(TRetorno);
            }
        }


        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T data, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return await httpClient.PutAsync(uri, content);
        }

        public async Task<T> GetAsync<T>(string uri, string token)
        {
            // Limpa os headers de autorização anteriores (caso tenha)
            httpClient.DefaultRequestHeaders.Authorization = null;

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }

            HttpResponseMessage response = await httpClient.GetAsync(uri);
            string serialized = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<T>(serialized);
            }
            else
            {
                throw new Exception($"Erro na requisição GET: {response.StatusCode} - {serialized}");
            }
        }




    }
}
