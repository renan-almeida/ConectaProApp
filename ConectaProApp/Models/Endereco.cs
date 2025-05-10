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
        public int IdEndereco { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public UfEnum Uf { get; set; }
        public string Cep { get; set; }
        public string Complemento { get; set; }
    }
}
