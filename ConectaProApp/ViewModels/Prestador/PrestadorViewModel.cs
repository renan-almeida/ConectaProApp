using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            System.Diagnostics.Debug.WriteLine($"Foto atribuída: {FotoPrestador}");
            FotoPrestador = string.IsNullOrEmpty(prestador.CaminhoFoto) ? "prestadorsemfoto.png" : prestador.CaminhoFoto;
            CriarPropostaCommand = new Command(async () =>  await CriarProposta());
        }

        public string FotoPrestador { get; set; }
        public string Nome { get; set; }
        public string Segmento { get; set; }


        public ICommand VerMaisCommand { get; set; }
        public ICommand CriarPropostaCommand { get; set; }

        private async Task CriarProposta()
        {
            if (prestador?.IdPrestador > 0)
            {
                try
                {
                    Debug.WriteLine("Id do prestador: " + prestador.IdPrestador);
                    // Aqui você passa o ID do prestador ou o objeto inteiro, como preferir
                    await Application.Current.MainPage.Navigation.PushAsync(new View.Cliente.PropostaClient(prestador.IdPrestador));
                }
                catch (Exception ex)
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", $"Falha ao abrir página: {ex.Message}", "OK");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "ID do prestador inválido.", "OK");
            }
        }


    }
}

