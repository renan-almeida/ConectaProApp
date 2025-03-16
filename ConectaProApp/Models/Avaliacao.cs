using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public class Avaliacao
    {
        public long idAvaliacao { get; set; }
        private long id_Servico;
        private long id_Cliente;
        private NvlSatisfacaoEnum nvlSatisfacao;

        public Avaliacao( Servico servico, EmpresaCliente cliente)
        {
            id_Servico = servico.IdServico;
            id_Cliente = cliente.IdUsuario;
        }
    }
}
