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
        
       EMERGÊNCIA,
       URGENTE,

        [Description("POUCO URGENTE")]
       POUCO_URGENTE,

        [Description("NÃO URGENTE")]
       NÃO_URGENTE
    }
}
