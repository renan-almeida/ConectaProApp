using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public class Avaliacao
    {
        public long IdAvaliacao { get; set; }
        public long Id_Servico { get; set; }
        public long Id_Cliente { get; set; }
        public NvlSatisfacaoEnum nvlSatisfacao { get; set; }

        public Avaliacao( Servico servico, EmpresaCliente cliente)
        {
            Id_Servico = servico.IdServico;
            Id_Cliente = cliente.IdUsuario;
        }
    }
}
