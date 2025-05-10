using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public enum StatusDisponibilidadeEnum
    {
        [Description("Disponivel")]
        DISPONIVEL,

        [Description("Indisponivel")]
        INDISPONIVEL,

        [Description("Em atendimento")]
        EM_ATENDIMENTO
    }
}
