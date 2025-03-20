﻿using ConectaProApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Services
{
    class UsuarioServices: Request
    {
        private readonly Request _request;
        private const string apiUrlBase = "xyz.com/ConectaPro/Usuarios";

        public UsuarioServices()
        {
            _request = new Request();
        }

        public async Task<Usuario> PostRegistrarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Registrar";
            u.IdUsuario = await _request
                .PostReturnIntAsync(apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }

        public async Task<Usuario> PostAutenticarUsuarioAsync(Usuario u)
        {
            string urlComplementar = "/Autenticar";

            u = await _request
                .PostAsync(apiUrlBase + urlComplementar, u, string.Empty);

            return u;
        }
    }
}
