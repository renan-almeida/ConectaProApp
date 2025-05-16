
using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }
        public TipoUsuarioEnum TipoUsuario { get; set; }
        public string CaminhoFoto { get; set; }
        public RoleEnum Role {get; set;}
        public string Token { get; set; }

    }
    
}
