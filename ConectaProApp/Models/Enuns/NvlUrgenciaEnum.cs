using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public enum NvlUrgenciaEnum
    {
        EMERGENTE = 1,

       URGENTE = 2,

        [Description("POUCO URGENTE")]
       POUCO_URGENTE = 3,

        [Description("NÃO URGENTE")]
       NÃO_URGENTE = 4
    }
}
