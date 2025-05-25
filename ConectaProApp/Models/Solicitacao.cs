using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Solicitacao
    {
        public int IdSolicitacao { get; set; }
        public Servico IdServico { get; set; }
        public Prestador IdPrestador { get; set; }
        public decimal ValorOrcamento { get; set; }
        public DateOnly PrevisaoInicio { get; set; }
        public int DuracaoServico { get; set; }
        public FormaPagtoEnum FormaPagtoEnum { get; set; }
        public StatusOrcamentoEnum StatusOrcamento { get; set; }
    }
}
