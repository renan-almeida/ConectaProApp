using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Storage;
using Azure.Storage.Blobs;
using System.Net.Http.Headers;

namespace ConectaProApp.Services.Azure
{
    public class BlobService
    {
        private readonly HttpClient _httpClient;
        private readonly string _uploadEndpoint = "https://conectapro-api.azurewebsites.net/fotos/upload";

        public BlobService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> UploadImagemAsync(FileResult file)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("Iniciando UploadImagemAsync...");

                if (file == null)
                {
                    System.Diagnostics.Debug.WriteLine("FileResult é nulo.");
                    return null;
                }

                System.Diagnostics.Debug.WriteLine($"Nome do arquivo: {file.FileName}, Tipo do arquivo: {file.ContentType}");

                // Verifica se o arquivo é uma imagem
                if (!file.ContentType.StartsWith("image/"))
                {
                    System.Diagnostics.Debug.WriteLine("Tipo de arquivo inválido.");
                    return null;
                }

                using var stream = await file.OpenReadAsync();
                System.Diagnostics.Debug.WriteLine($"Stream aberto. Comprimento: {stream.Length}");

                using var content = new MultipartFormDataContent();

                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType); // Define o ContentType dinamicamente

                // Nome do campo precisa bater com o esperado pelo back-end
                content.Add(fileContent, "arquivo", file.FileName);

                System.Diagnostics.Debug.WriteLine("Enviando requisição para o endpoint de upload...");
                var response = await _httpClient.PostAsync(_uploadEndpoint, content);

                // Log detalhado do status da resposta
                System.Diagnostics.Debug.WriteLine($"StatusCode da resposta: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    var url = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Upload realizado com sucesso. URL retornada: {url}");
                    return url; // URL pública da imagem retornada pelo backend
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Erro no upload - StatusCode: {response.StatusCode}, Mensagem: {erro}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Exceção em UploadImagemAsync: {ex.Message}");
                return null;
            }
        }
    }
}
