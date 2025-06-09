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
    public enum UfEnum
    {
        AC, AL, AP, AM, BA, CE, DF, ES, GO, MA, MT,
        MS, MG, PA, PB, PR, PE, PI, RJ, RN, RS,
        RO, RR, SC, SP, SE, TO
    }
}
