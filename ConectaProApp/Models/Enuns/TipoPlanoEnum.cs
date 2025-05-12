using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public enum TipoPlanoEnum
    {
        [Description ("Premium")]
        PREMIUM = 1,

        [Description ("Padrão")]
        PADRÃO = 2,

        [Description("Básico")]
        BÁSICA = 3



    }
}
