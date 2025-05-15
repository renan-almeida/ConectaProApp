using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class EmpresaCreateDTO
    {
        public string Nome { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public List<Servico> Servicos { get; set; }
        public string Email { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }
        public string Senha { get; set; }

        // Endereco
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public UfEnum Uf { get; set; }


    }
}
