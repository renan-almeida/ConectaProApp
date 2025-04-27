using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
    public enum FormaPagtoEnum
    {
        [Description("Cartão de Credito")]
        CRÉDITO,

        [Description("Cartão de Debito")]
        DÉBITO,

        [Description("Pix")]
        PIX,

        [Description("Boleto")]
        BOLETO

       
    }
}
