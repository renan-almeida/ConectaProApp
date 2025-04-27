using ConectaProApp.Models;
using System.Threading.Tasks;

namespace ConectaProApp.Services.Cliente
{
    class ClienteService : Request
    {
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";

        public async Task<EmpresaCliente> PostRegistrarClienteAsync(EmpresaCliente e)
        {
            string urlComplementar = "/RegistrarCliente";

            e.IdUsuario = await PostReturnIntAsync(apiUrlBase + urlComplementar, e, string.Empty);

            return e;
        }

      
    }
}
