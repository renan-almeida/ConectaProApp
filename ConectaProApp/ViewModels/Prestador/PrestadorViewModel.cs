using ConectaProApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using prestadorModel = ConectaProApp.Models.Prestador;

namespace ConectaProApp.ViewModels.Prestador
{
    public class PrestadorViewModel: BaseViewModel
    {
        private readonly prestadorModel prestador;

        public PrestadorViewModel(prestadorModel prestador)
        {
            this.prestador = prestador;
            Nome = prestador.Nome;
            Segmento = prestador.Segmento ?? "Indefinido";
            FotoPrestador = string.IsNullOrEmpty(prestador.CaminhoFoto) ? "prestadorsemfoto.png" : prestador.CaminhoFoto;
        }

        public string FotoPrestador {get; set;}
        public string Nome { get; set; }
        public string Segmento { get; set; }

        

        public ICommand VerMaisCommand { get; set; }
        public ICommand CriarPropostaCommand { get; set; }


    }
}
