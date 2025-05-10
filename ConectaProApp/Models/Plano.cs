using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Plano
    {
        public int IdPlano { get; set; }
        public string DescPlano { get; set; }
        public float ValorFixoPlano { get; set; }
        public float PercentualPlano { get; set; }
        public TipoPlanoEnum TipoPlano { get; set; }
    }
}
