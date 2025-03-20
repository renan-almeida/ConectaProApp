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
        public long Id_Plano { get; set; }
        public string DescPrestador { get; set; }
        public string Cpf { get; set; }
        public List<string> Especialização { get; set; }
        public StatusDisponibilidadeEnum StatusDisponibilidade { get; set; }

        public Prestador( Plano plano)
        {
            Id_Plano = plano.IdPlano; 
        }
    }
}
