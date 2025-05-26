using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public enum NvlSatisfacaoEnum
    {
        [Description("Muito satisfeito")]
        MUITO_SATISFEITO = 1,

        [Description("Satisfeito")]
        SATISFEITO = 2,

        [Description("Pouco satisfeito")]
        POUCO_SATISFEITO = 3,

        [Description("Insatisfeito")]
        INSATISFEITO = 4
    }
}
