using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace ConectaProApp.Models.Enuns
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum StatusOrcamentoEnum
    {
        ATIVA = 4 ,
        INATIVA = 5,
        ACEITA = 6,
        PENDENTE = 7,
        RECUSADA = 8,
        FINALIZADA = 9

    }
}
