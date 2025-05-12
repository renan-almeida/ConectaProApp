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

        LIMPEZA = 1,

        [Description("Reparos")]
        REPAROS = 2,

        [Description("Tecnologia")]
        TECNOLOGIA = 3,

        [Description("Design")]
        DESIGN = 4,

        [Description("Construção")]
        CONSTRUÇÃO = 5,

        [Description("Jardinagem")]
        JARDINAGEM = 6,

        [Description("Pintura")]
        PINTURA = 7,

        [Description("Mecânico")]
        MECANICO = 8,

        [Description("Motorista")]
        MOTORISTA = 9,

        [Description("Outros")]
        OUTROS = 10
    }
}

