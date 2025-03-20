using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
   public  class EmpresaCliente: Usuario
    {
        public long IdEmpresa { get; set; }
        public string Cnpj { get; set; } = string.Empty;
        public string NomeFantasia { get; set; } = string.Empty;
    }
}
