using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class PrestadorResponseDTO
    {
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Telefone { get; set; }
        public string DescPrestador { get; set; }
        public DateTime DataNascimento { get; set; }
        public StatusDisponibilidadeEnum StatusDisponibilidade { get; set; }
        public List<string> Especialidades { get; set; }
        public List<string> Habilidades { get; set; }

        public List<int> Segmentos { get; set; } // ou lista de objetos se vier com descrição

        public Plano IdPlano { get; set; }
        public Endereco Endereco { get; set; }

    }
}
