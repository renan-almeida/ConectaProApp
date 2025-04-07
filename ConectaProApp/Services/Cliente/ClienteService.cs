using ConectaProApp.Models;
using System.Threading.Tasks;

namespace ConectaProApp.Services.Cliente
{
    class ClienteService : Request
    {
        private const string apiUrlBase = "xyz.com/ConectaPro/Usuarios/Clientes";

        public async Task<EmpresaCliente> PostRegistrarUsuarioAsync(EmpresaCliente e)
        {
            string urlComplementar = "/RegistrarCliente";

            e.IdUsuario = await PostReturnIntAsync(apiUrlBase + urlComplementar, e, string.Empty);

            return e;
        }
    }
}
