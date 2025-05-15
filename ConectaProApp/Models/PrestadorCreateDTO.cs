using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class PrestadorCreateDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Senha { get; set; }
        public string Telefone { get; set; }
        public DateTime DataNascimento { get; set; }
        public string DescPrestador { get; set; }
        public List<string> Especialidades { get; set; }
        public List<string> Habilidades { get; set; }
        public List<int> Segmentos { get; set; }
        public StatusDisponibilidadeEnum StatusDisponibilidade { get; set; }
        public long IdPlano { get; set; }

        // Endereço achatado
        public string Cep { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public UfEnum Uf { get; set; }
    }
}

