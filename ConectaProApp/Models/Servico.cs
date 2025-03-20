using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Servico
    {
        public long IdServico { get; set; }
        public long Id_Plano { get; set; }
        public long Id_Segmento { get; set; }
        public long Id_Prestador { get; set; }
        public long Id_Cliente { get; set; }
        public long Id_Endereco { get; set; }
        public float VcServico { get; set; }
        public DateTime DiServico { get; set; }
        public DateTime DaServico { get; set; }
        public DateTime DeServico { get; set; }
        public DateTime DpServico { get; set; }
        public StatusServicoEnum StServico { get; set; }
        public NvlUrgenciaEnum NvlUrgenciaEnum { get; set; }

        public Servico(Plano plano, Segmento segmento, Prestador prestador, EmpresaCliente cliente)
        {
            Id_Plano = plano.IdPlano;
            Id_Segmento = segmento.IdSegmento;
            Id_Prestador = prestador.IdPrestador;
            Id_Cliente = cliente.IdEmpresa;
            
        }
    }
}
