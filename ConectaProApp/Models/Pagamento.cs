using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Pagamento
    {
        public int  IdPagamento { get; set; }
        public Servico IdServico { get; set; }
        public float ValorPagamento { get; set; }
        public float ValorPlataforma { get; set; }
        public float ValorPrestador { get; set; }
        public string SituacaoRepasse { get; set; }
    }
}
