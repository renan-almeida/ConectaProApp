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
        public int Telefone { get; set; }
        public byte[] FotoUrl{ get; set; }
        public TipoUsuarioEnum ipoUsuario { get; set; }
    }
}
