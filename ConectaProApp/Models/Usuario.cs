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
        public long IdUsuario { get; private set; }
        private string nome;
        private string Cnpj;
        private string email;
        private string senha;
        private int telefone;
        private string fotoUrl;
        private TipoUsuarioEnum tipoUsuario;
    }
}
