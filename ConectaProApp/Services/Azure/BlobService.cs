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
                if (file == null)
                    return null;

                using var stream = await file.OpenReadAsync();
                using var content = new MultipartFormDataContent();

                var fileContent = new StreamContent(stream);
                fileContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg"); // ou "image/png" dependendo do tipo

                // "arquivo" precisa bater com o nome do parâmetro no back-end: @RequestParam("arquivo")
                content.Add(fileContent, "arquivo", file.FileName);

                var response = await _httpClient.PostAsync(_uploadEndpoint, content);

                if (response.IsSuccessStatusCode)
                {
                    var url = await response.Content.ReadAsStringAsync();
                    return url; // URL pública da imagem retornada pelo Spring
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Erro no upload: {erro}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro no BlobService: {ex.Message}");
                return null;
            }
        }
    }
}
