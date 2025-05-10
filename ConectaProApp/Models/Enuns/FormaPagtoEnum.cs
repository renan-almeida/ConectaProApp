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
        [Description("Cartão")]
        CARTÃO,

        [Description("Dinheiro")]
        DINHEIRO,

        [Description("Pix")]
        PIX,

        [Description("Boleto")]
        BOLETO

       
    }
}
