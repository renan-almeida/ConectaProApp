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
        public long IdPrestador { get; set; }
        public long? Id_Plano { get; set; }
        public TipoPlanoEnum TipoPlano { get; set; }
        public string DescPrestador { get; set; }
        public List<TipoSegmentoEnum> Segmento { get; set; }
        public ObservableCollection<string> Habilidades { get; set; }
        public string Cpf { get; set; }
        public ObservableCollection<string> Especializacoes { get; set; }
        public DateTime DataNascimento { get; set; }
        public StatusDisponibilidadeEnum StatusDisponibilidade { get; set; }
    }
}
