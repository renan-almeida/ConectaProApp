using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Prestador: Usuario
    {
        public long IdPrestador { get; set; }
        private long id_Plano;
        private string descPrestador;
        private string prestador;
        private List<string> especialização;
        private StatusDisponibilidadeEnum statusDisponibilidade;

        public Prestador( Plano plano)
        {
            id_Plano = plano.IdPlano; 
        }
    }
}
