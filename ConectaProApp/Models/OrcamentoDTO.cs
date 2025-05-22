using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class OrcamentoDTO
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdPrestador { get; set; }
        public string Descricao { get; set; }
        public decimal ValorProposto { get; set; }
        public StatusOrcamentoEnum StatusOrcamentoEnum { get; set; } // PENDENTE, ACEITO, RECUSADO
        public DateTime DataCriacao { get; set; }
    }
}
