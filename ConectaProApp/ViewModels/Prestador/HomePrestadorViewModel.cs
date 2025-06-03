using ConectaProApp.Models;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Prestador;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;
using ServicoModel = ConectaProApp.Models.Servico;

namespace ConectaProApp.ViewModels.Prestador
{
    public partial class HomePrestadorViewModel : BaseViewModel
    {
        private readonly ServicoService sService;

        private List<ServicoHomeDTO> servicosUf;
        private int indice = 0;
        private string uf = Preferences.Get("uf", string.Empty);

        public ICommand BuscarServicosCommand { get; }
        public ICommand ProximoServicoCommand { get; }
        public ICommand CandidatarServicoCommand { get; }

        public HomePrestadorViewModel()
        {
            sService = new ServicoService();

            BuscarServicosCommand = new Command(async () => await BuscarServicosAsync());
            ProximoServicoCommand = new Command(MostrarProximoServico);
            CandidatarServicoCommand = new Command(async () =>
            {
                if (ServicoAtual?.IdSolicitacao > 0)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CandidatarPrestador(ServicoAtual.IdSolicitacao));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Nenhum serviço selecionado.", "OK");
                }
            });

            _ = CarregarFotoPrestadorAsync(); // Carregamento inicial
        }

        [ObservableProperty]
        private ServicoHomeDTO servicoAtual;

        [ObservableProperty]
        private ImageSource fotoPrestadorUrl;

        public string NomeFantasia => string.IsNullOrWhiteSpace(ServicoAtual?.EmpresaClienteResumoDTO?.NomeFantasia)
            ? "Empresa sem nome" : ServicoAtual.EmpresaClienteResumoDTO.NomeFantasia;

        public string CaminhoFotoEmpresa => string.IsNullOrWhiteSpace(ServicoAtual?.EmpresaClienteResumoDTO?.CaminhoFoto)
            ? "empresasemfoto.png" : ServicoAtual.EmpresaClienteResumoDTO.CaminhoFoto;

        public string CaminhoFotoFundo => CaminhoFotoEmpresa;

        public string DescSolicitacao => string.IsNullOrWhiteSpace(ServicoAtual?.DescSolicitacao)
            ? "sem descrição" : ServicoAtual.DescSolicitacao;

        public string Uf => string.IsNullOrWhiteSpace(uf) ? "Sem Uf" : uf;

        public decimal ValorProposto => ServicoAtual.ValorProposto;

        public async Task InitAsync()
        {
            await CarregarFotoPrestadorAsync();
            await BuscarServicosAsync();
        }

        private async Task BuscarServicosAsync()
        {
            uf = Preferences.Get("uf", string.Empty);
            servicosUf = await sService.BuscarSolictacaoPorUfAsync(uf);

            if (servicosUf.Any())
            {
                indice = 0;
                ServicoAtual = servicosUf[indice];
                NotificarTudo();
            }
            else
            {
                ServicoAtual = null;
                await Application.Current.MainPage.DisplayAlert("Aviso", "Nenhum serviço encontrado para sua região.", "OK");
            }
        }

        private void MostrarProximoServico()
        {
            if (servicosUf == null || servicosUf.Count == 0) return;

            indice = (indice + 1) % servicosUf.Count;
            ServicoAtual = servicosUf[indice];
            NotificarTudo();
        }

        private async Task CarregarFotoPrestadorAsync()
        {
            // TODO: Implementar carregamento real se desejar buscar do backend.
            FotoPrestadorUrl = "prestadorsemfoto.png";
        }

        private void NotificarTudo()
        {
            OnPropertyChanged(nameof(NomeFantasia));
            OnPropertyChanged(nameof(CaminhoFotoEmpresa));
            OnPropertyChanged(nameof(CaminhoFotoFundo));
            OnPropertyChanged(nameof(DescSolicitacao));
            OnPropertyChanged(nameof(Uf));
        }
    }
}
