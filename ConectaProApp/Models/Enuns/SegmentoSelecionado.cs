using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models.Enuns
{
   public  class SegmentoSelecionado
    {
        public string Nome { get; set; } // [Description]
        public TipoSegmentoEnum Segmento { get; set; } // Qual segmento está selecionado
        public bool Selecionado { get; set; } // Se está selecionado ou não
    }
}
