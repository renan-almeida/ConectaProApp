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
        private long id_Plano;
        private long id_Segmento; 
        private long id_Prestador;    
        private long id_Cliente;   
        private float vcServico;
        private DateTime diServico;
        private DateTime daServico;
        private DateTime deServico;
        private DateTime dpServico;
        private char stServico;

        public Servico(Plano plano, Segmento segmento, Prestador prestador, EmpresaCliente cliente)
        {
            id_Plano = plano.IdPlano;
            id_Segmento = segmento.IdSegmento;
            id_Prestador = prestador.IdPrestador;
            id_Cliente = cliente.IdEmpresa;
            
        }
    }
}
