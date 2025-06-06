using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ConectaProApp.Models.Enuns
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum TipoSegmentoEnum
    {
        [Description("Tecnologia")]

        TECNOLOGIA = 1,

        [Description("Marketing")]
        MARKETING = 2,

        [Description("Design")]
        DESIGN = 3,

        [Description("Redes")]
        REDES = 4,

        [Description("Limpeza")]
        LIMPEZA = 5,

        [Description("Telecomunicações")]
        TELECOMUNICAÇÕES = 6,

        [Description("Segurança")]
        SEGURANÇA = 7,

        [Description("Monitoramento")]
        MONITORAMENTO = 8,

        [Description("Residencial")]
        RESIDENCIAL = 9,

        [Description("Refrigeração")]
        REFRIGERAÇÃO = 10,

        [Description("Eletrica")]
        ELETRICA = 11,

        [Description("Construção")]
        CONSTRUÇÃO = 12,

        [Description("Reparos")]
        REPAROS = 13,

        [Description("Jardinagem")]
        JARDINAGEM = 14,

        [Description("Contabil")]
        CONTABIL = 15,

        [Description("Beleza")]
        BELEZA = 16,

        [Description("Estetica")]
        ESTETICA = 17,

        [Description("Outros")]
        OUTROS = 18,

        [Description("Automotivo")]
        AUTOMOTIVO = 19

    }
}

