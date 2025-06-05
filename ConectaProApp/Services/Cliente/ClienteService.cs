using ConectaProApp.Models;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Services.Cliente
{
    class ClienteService : Request
    {
        private readonly ApiService _apiService;
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";

        public async Task<EmpresaCreateDTO> PostRegistrarClienteAsync(EmpresaCreateDTO e)
        {
            string urlComplementar = "/empresaCliente/registro";

            var clienteRegistrado = await PostAsyncFlex<EmpresaCreateDTO, EmpresaCreateDTO>(
                apiUrlBase + urlComplementar, e, string.Empty);

            return clienteRegistrado;
        }

        public async Task<EmpresaResponseDTO> GetEmpresaByIdAsync(int id, string token)
        {
            string url = $"https://conectapro-api.azurewebsites.net/empresaCliente/{id}";
            return await GetAsync<EmpresaResponseDTO>(url, token);
        }



    }
}
