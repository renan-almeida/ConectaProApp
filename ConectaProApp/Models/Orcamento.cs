using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Orcamento
    {
        public long idOrcamento { get; set; }
        private long id_Plano;
        private long id_Servico;
        private long id_Prestador;
        private float vlOrcamento;
        private DateTime piOrcamento;
        private int duOrcamento;
        private StatusOrcamentoEnum statusOrcamento;

    }
}
