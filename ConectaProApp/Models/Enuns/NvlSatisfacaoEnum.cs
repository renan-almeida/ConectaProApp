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
        MUITO_SATISFEITO,

        [Description("Satisfeito")]
        SATISFEITO,

        [Description("Pouco satisfeito")]
        POUCO_SATISFEITO,

        [Description("Insatisfeito")]
        INSATISFEITO
    }
}
