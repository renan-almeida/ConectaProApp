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
        private readonly PrestadorResponseBuscaDTO prestador;

        public PrestadorViewModel(PrestadorResponseBuscaDTO prestador)
        {
            this.prestador = prestador;

        }

        public string FotoPrestador => prestador.CaminhoFoto;
        public string Nome => prestador.Nome;
        public string Segmento => prestador.Segmento;

        public ICommand VerMaisCommand { get; set; }
        public ICommand CriarPropostaCommand { get; set; }


    }
}
