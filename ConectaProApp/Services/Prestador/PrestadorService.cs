using ConectaProApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Services.Prestador
{
    public class PrestadorService : Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";

        public PrestadorService()
        {
            _request = new Request();
        }

        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario p)
        {
            string urlComplementar = "/RegistrarPrestador";
            p.IdUsuario = await _request
                .PostReturnIntAsync(apiUrlBase + urlComplementar, p, string.Empty);

            return p;
        }
    }
}
