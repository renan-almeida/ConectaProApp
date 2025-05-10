using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public class Avaliacao
    {
        public long Id { get; set; }
        public string Descricao { get; set; }
        public Servico IdServico { get; set; }
        public EmpresaCliente IdEmpresaCliente { get; set; }
        public NvlSatisfacaoEnum nvlSatisfacao { get; set; }
        public Prestador IdPrestador { get; set; }

    }
}
