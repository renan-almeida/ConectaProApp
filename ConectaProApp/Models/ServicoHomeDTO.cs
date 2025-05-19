using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class ServicoHomeDTO
    {
        public int IdServico { get; set; }
        public string DescServico { get; set; }
        public string CaminhoFoto { get; set; }
        public string NomeFantasia { get; set; }
        public string Uf { get; set; }
    }
}
