using ConectaProApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Services.Cliente
{
    class ClienteService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "xyz.com/ConectaPro/Usuarios/Clientes";

        public ClienteService()
        {
            _request = new Request();
        }

        public async Task<EmpresaCliente> PostRegistrarUsuarioAsync(EmpresaCliente e)
        {
            string urlComplementar = "/RegistrarCliente";
            e.IdUsuario = await _request
                .PostReturnIntAsync(apiUrlBase + urlComplementar, e, string.Empty);

            return e;
        }

    }
}
