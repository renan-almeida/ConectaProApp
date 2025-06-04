using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public enum StatusServicoEnum
    {
        ORCAMENTO = 1,
        RECUSADO = 2,
        PendentePagto = 3,
        PendenteInicio = 4,
        EM_EXECUCAO = 5,
        PendenteConfirmarFinalizacao = 6,
        FINALIZADO = 7,
        Agendado = 8,
        Cancelado = 9
    }
}
