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
    public enum TipoPlanoEnum
    {
        [Description ("Standard")]
        STANDARD = 1,

        [Description ("Premium")]
        PREMIUM = 2,

        [Description("Pro")]
        PRO = 3



    }
}
