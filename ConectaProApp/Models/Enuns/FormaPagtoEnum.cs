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
        CARTÃO = 1,

        [Description("Dinheiro")]
        DINHEIRO = 2,

        [Description("Pix")]
        PIX = 3,

        [Description("Boleto")]
        BOLETO = 4

       
    }
}
