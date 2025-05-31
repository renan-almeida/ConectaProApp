using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Azure.Storage.Blobs;
using System.Net.Http.Headers;
using System.Diagnostics;
using ConectaProApp.Services;

namespace ConectaProApp.Services.Azure
{
    public class BlobService
    {
        
        private readonly ApiService _apiService;
        private readonly string _uploadEndpoint = "https://conectapro-api.azurewebsites.net/fotos/upload";

        public BlobService(ApiService apiService)
        {
           
            _apiService = apiService;
        }

        public async Task<string> UploadImagemAsync(FileResult file)
        {
            try
            {
                Debug.WriteLine("Iniciando UploadImagemAsync...");

                if (file == null)
                {
                    Debug.WriteLine("FileResult é nulo.");
                    return null;
                }

                Debug.WriteLine($"Nome do arquivo: {file.FileName}, Tipo do arquivo: {file.ContentType}");

                if (!file.ContentType.StartsWith("image/"))
                {
                    Debug.WriteLine("Tipo de arquivo inválido.");
                    return null;
                }

                using var stream = await file.OpenReadAsync();
                Debug.WriteLine($"Stream aberto. Comprimento: {stream.Length}");

                using var content = new MultipartFormDataContent();
                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                content.Add(fileContent, "arquivo", file.FileName);

                await _apiService.ConfigureAuthorizationHeaderAsync(); // 🔹 Configura antes de enviar
                Debug.WriteLine($"🔹 Cabeçalho de autorização configurado antes do upload");

                var response = await _apiService.HttpClient.PostAsync(_uploadEndpoint, content);

                Debug.WriteLine($"StatusCode da resposta: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var url = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Upload realizado com sucesso. URL retornada: {url}");
                    return url;
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine($"Erro no upload - StatusCode: {response.StatusCode}, Mensagem: {erro}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Exceção em UploadImagemAsync: {ex.Message}");
                return null;
            }
        }
    }


}

