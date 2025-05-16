using ConectaProApp.Models;
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
        private const string apiUrlBase = "https://conectapro-api.azurewebsites.net";

        public UsuarioServices()
        {
            _request = new Request();
        }
        public async Task<LoginResponseDTO> PostAutenticarUsuarioAsync(LoginRequestDTO u)
        {
            try
            {
                string urlComplementar = "/login";
                var usuarioAutenticado = await _request
                    .PostAsyncFlex<LoginRequestDTO, LoginResponseDTO>(apiUrlBase + urlComplementar, u, string.Empty);

                return usuarioAutenticado;
            }
            catch (Exception ex)
            {
                // Aqui você pode logar o erro, se quiser
                Console.WriteLine($"Erro ao autenticar: {ex.Message}");
                    throw;
            }
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
