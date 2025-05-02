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
        public long IdPlano { get; set; }
        public TipoPlanoEnum TipoPlano { get; set; }
        public float VfPlano { get; set; }
        public float PlPlano { get; set; }
    }
}
