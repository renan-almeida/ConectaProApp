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
        public long idEndereco { get; set; }
        private long id_Usuario;
        private string logradouro;
        private int nro;
        private string bairro;
        private string cidade;
        private UfEnum estado;
    }
}
