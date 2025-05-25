using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
     public class SolicitacaoCreateDTO
    {
        public int IdSolicitacao { get; set; }
        public int IdCliente { get; set; }
        public int IdPrestador { get; set; }
        public string Descricao { get; set; }
        public decimal ValorProposto { get; set; }
    }
}

