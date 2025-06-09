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
    public enum StatusDisponibilidadeEnum
    {
        [Description("DisponÍvel")]
        DISPONIVEL,

        [Description("Indisponivel")]
        INDISPONIVEL,

        [Description("Em atendimento")]
        EM_ATENDIMENTO
    }
}
