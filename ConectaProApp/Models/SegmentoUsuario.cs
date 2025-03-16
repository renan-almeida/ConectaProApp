using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class SegmentoUsuario
    {
        private long Id_Usuario { get; set; }
        private long Id_Segmento { get; set; }

        public SegmentoUsuario(Usuario usuario, Segmento segmento)
        {
            Id_Usuario = usuario.IdUsuario;
            Id_Segmento = segmento.IdSegmento;
        }
    }
}
