using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class Prestador: Usuario
    {
        public string DescPrestador { get; set; }
        public string Cpf { get; set; }
        public List<string> Especialidades { get; set; }
        public ObservableCollection<string> Habilidades { get; set; }
        public List<int> Segmentos { get; set; }
        public DateTime DataNascimento { get; set; }
        public StatusDisponibilidadeEnum StatusDisponibilidade { get; set; }
        public long IdPlano { get; set; }
        public List<Avaliacao> Avaliacoes { get; set; }
        public List<Orcamento> Orcamentos { get; set; }
        public List<Servico> Servicos { get; set; }
    }
}
