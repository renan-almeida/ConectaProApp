using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Endereco
    {
        public long IdEndereco { get; set; }
        public long Id_Usuario { get; set; }
        public string Logradouro { get; set; }
        public int Nro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public UfEnum Estado { get; set; }

        public Endereco( Usuario usuario)
        {
            Id_Usuario = usuario.IdUsuario;
        }
    }
}
