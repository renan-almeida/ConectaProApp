using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
   public  class EmpresaCliente: Usuario
    {
        public string Cnpj { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public List<Servico> Servicos { get; set; }
       
        public string CaminhoAvatar { get; set; }

        public string CaminhoHeader { get; set; }

    }
}
