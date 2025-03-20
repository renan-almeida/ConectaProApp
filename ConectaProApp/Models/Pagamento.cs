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
        public long  IdPagamento { get; set; }
        public long Id_Servico { get; set; }
        public double VlPagamento { get; set; }
        public double VlPlataforma { get; set; }
        public double VlPrestador { get; set; }

        public Pagamento(Servico servico)
        {
            Id_Servico = servico.IdServico;
            
        }


    }
}
