using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public enum TipoSegmentoEnum
    {
        [Description("Limpeza")]
        LIMPEZA,

        [Description("Reparos")]
        REPAROS,

        [Description("Tecnologia")]
        TECNOLOGIA,

        [Description("Design")]
        DESIGN,

        [Description("Construção")]
        CONSTRUÇÃO,

        [Description("Jardinagem")]
        JARDINAGEM,

        [Description("Outros")]
        OUTROS
    }
}

