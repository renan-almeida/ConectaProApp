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
        public long IdOrcamento { get; set; }
        public long Id_Plano { get; set; }
        public long Id_Servico { get; set; }
        public long Id_Prestador { get; set; }
        public float VlOrcamento { get; set; }
        public DateTime PiOrcamento { get; set; }
        public int DuOrcamento { get; set; }
        public StatusOrcamentoEnum StatusOrcamento { get; set; }

        public Orcamento( Plano plano, Servico servico, Prestador prestador)
        {
            Id_Plano = plano.IdPlano;
            Id_Servico = servico.IdServico;
            Id_Prestador = prestador.IdPrestador;
        }
    }
}
