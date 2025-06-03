using ConectaProApp.Models;
using ConectaProApp.ViewModels.Prestador;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ConectaProApp.ViewModels.Cliente
{
    [QueryProperty(nameof(PrestadoresOriginais), "Prestadores")]
    [QueryProperty(nameof(TituloBusca), "TituloBusca")]
    public partial class ResultadoBuscaClientViewModel : BaseViewModel
    {
        [ObservableProperty]
        private List<PrestadorResponseBuscaDTO> prestadoresOriginais;

        public string FotoEmpresaUrl { get; set; }

        [ObservableProperty]
        private string tituloBusca;

        public ObservableCollection<PrestadorViewModel> Prestadores { get; } = new();

        public ResultadoBuscaClientViewModel()
        {
            // Lista inicializada acima
        }

        // Este método é gerado automaticamente quando a propriedade "PrestadoresOriginais" muda
        partial void OnPrestadoresOriginaisChanged(List<PrestadorResponseBuscaDTO> value)
        {
            CarregarPrestadores();
        }

        private async Task CarregarFotoEmpresaAsync()
        {

            FotoEmpresaUrl = "empresasemfoto.png";

        }


        private void CarregarPrestadores()
        {
            Prestadores.Clear();

            if (prestadoresOriginais == null)
                return;

            foreach (var p in prestadoresOriginais)
            {
                Prestadores.Add(new PrestadorViewModel(p));
            }
        }
    }
}
