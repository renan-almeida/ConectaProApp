﻿using ConectaProApp.Models;
using Newtonsoft.Json;
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
            string urlComplementar = "/RegistrarCliente";
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

        public class EmailRequest
        {
            public string Email { get; set; }
        }

        public async Task<bool> SolicitarCodigoRecuperacaoSenhaAsync(string email)
        {
            var uri = $"{apiUrlBase}/SolicitarCodigoRecuperacaoSenha";
            var response = await _request.PostAsync<dynamic>(uri, new EmailRequest { Email = email }, string.Empty);

            Console.WriteLine($"Response: {response}");
            return response.success ?? false; // Ajuste conforme o formato real da resposta
        }
    }
}
