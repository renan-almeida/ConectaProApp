
using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Usuario
    {
        public long IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Token { get; set; }
        public string  Telefone { get; set; }
        public string Cep { get; set; }
        public int Nro { get; set; }
        public  UfEnum Uf { get; set; }
        public byte[] FotoUrl{ get; set; }
        public TipoUsuarioEnum TipoUsuario { get; set; }
    }
}
