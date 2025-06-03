using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using prestadorModel = ConectaProApp.Models.Prestador;

namespace ConectaProApp.ViewModels.Prestador
{
    public class PrestadorViewModel : BaseViewModel
    {
        private readonly PrestadorResponseBuscaDTO prestador;

        public PrestadorViewModel(PrestadorResponseBuscaDTO prestador)
        {
            this.prestador = prestador;
            Nome = string.IsNullOrEmpty(prestador.Nome) ? "Sem nome" : prestador.Nome;
            Segmento = prestador.TipoCategoria != null && prestador.TipoCategoria.Any()
             ? string.Join(", ", prestador.TipoCategoria)
            : "Sem segmento";

            FotoPrestador = string.IsNullOrEmpty(prestador.CaminhoFoto) ? "prestadorsemfoto.png" : prestador.CaminhoFoto;

            System.Diagnostics.Debug.WriteLine($"Foto atribuída: {FotoPrestador}");

        }

        public string FotoPrestador { get; set; }
        public string Nome { get; set; }
        public string Segmento { get; set; }


        public ICommand VerMaisCommand { get; set; }
        public ICommand CriarPropostaCommand { get; set; }
    }
}

