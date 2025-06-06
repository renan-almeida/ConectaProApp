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
    public enum FormaPagtoEnum
    {
        [Description("Dinheiro")]
        DINHEIRO = 1,

        [Description("Debito")]
        DEBITO = 2,

        [Description("Credito")]
        CREDITO = 3,

        [Description("Pix")]
        PIX = 4,

        [Description("Boleto")]
        BOLETO = 5


    }
}
