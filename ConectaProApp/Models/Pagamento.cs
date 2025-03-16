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
        private long id_Servico;
        private double vlPagamento;
        private double vlPlataforma;
        private double vlPrestador;

        public Pagamento(Servico servico)
        {
            id_Servico = servico.IdServico;
            
        }


    }
}
