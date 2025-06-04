using ConectaProApp.Models;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Cliente;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ConectaProApp.ViewModels.Cliente
{
    public partial class HomeClientViewModel : BaseViewModel
    {
        private readonly ServicoService sService;

        public ICommand PropostaCommand { get;  set; }
        public ICommand CriarServicoCommand { get;  set; }
        public ICommand ProximoPrestadorCommand { get;  set; }

        private List<PrestadorResponseBuscaDTO> prestadorUf;
        private int indice = 0;

        public HomeClientViewModel()
        {
            sService = new ServicoService();
            InitializeCommands();

            Task.Run(CarregarDadosIniciaisAsync);
        }

        private async Task CarregarDadosIniciaisAsync()
        {
            await BuscarPrestadorAsync();
            await CarregarFotoEmpresaAsync();
        }

        private void InitializeCommands()
        {
            CriarServicoCommand = new Command(async () => await CriarSolicitacaoView());
            ProximoPrestadorCommand = new Command(() => MostrarProximoPrestador());
            PropostaCommand = new Command(async () =>
            {
                if (PrestadorAtual?.IdPrestador > 0)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new PropostaClient(PrestadorAtual.IdPrestador));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Nenhum prestador selecionado.", "OK");
                }
            });
        }

        [ObservableProperty]
        private PrestadorResponseBuscaDTO prestadorAtual;

        [ObservableProperty]
        private ImageSource fotoPrestadorUrl;

        [ObservableProperty]
        private ImageSource fotoEmpresaUrl;

        private async Task BuscarPrestadorAsync()
        {
            try
            {
                var uf = Preferences.Get("uf", string.Empty);
                prestadorUf = await sService.BuscarPrestadorUfAsync(uf);

                if (prestadorUf?.Any() == true)
                {
                    indice = 0;
                    MostrarProximoPrestador();
                    NotificarTudo();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Nenhum prestador encontrado para sua UF.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Erro ao buscar prestadores: {ex.Message}", "OK");
            }
        }

        private void MostrarProximoPrestador()
        {
            if (prestadorUf == null || prestadorUf.Count == 0) return;

            if (indice >= prestadorUf.Count) indice = 0;

            PrestadorAtual = prestadorUf[indice];
            _ = CarregarFotoPrestadorAsync();
                NotificarTudo();
            indice++;
        }

        private async Task CarregarFotoPrestadorAsync()
        {
            if (!string.IsNullOrWhiteSpace(PrestadorAtual?.CaminhoFoto))
            {
                try
                {
                    var bytes = Convert.FromBase64String(PrestadorAtual.CaminhoFoto);
                    FotoPrestadorUrl = ImageSource.FromStream(() => new MemoryStream(bytes));
                }
                catch
                {
                    FotoPrestadorUrl = ImageSource.FromFile("prestadorsemfoto.png");
                }
            }
            else
            {
                FotoPrestadorUrl = ImageSource.FromFile("prestadorsemfoto.png");
            }
        }

        private async Task CarregarFotoEmpresaAsync()
        {
            try
            {
                var fotoBase64 = await SecureStorage.GetAsync("FotoEmpresa");

                if (!string.IsNullOrEmpty(fotoBase64))
                {
                    var bytes = Convert.FromBase64String(fotoBase64);
                    FotoEmpresaUrl = ImageSource.FromStream(() => new MemoryStream(bytes));
                }
                else
                {
                    FotoEmpresaUrl = ImageSource.FromFile("empresasemfoto.png");
                }
            }
            catch
            {
                FotoEmpresaUrl = ImageSource.FromFile("empresasemfoto.png");
            }
        }

        private async Task CriarSolicitacaoView()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CriarSolicitacaoClient());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Erro ao abrir tela: {ex.Message}", "OK");
            }
        }

        private void NotificarTudo()
        {
            OnPropertyChanged(nameof(NomePrestador));
            OnPropertyChanged(nameof(FotoPrestadorUrl));
            OnPropertyChanged(nameof(DescPrestador));
            OnPropertyChanged(nameof(Uf));
        }
        public string NomePrestador => string.IsNullOrWhiteSpace(PrestadorAtual?.Nome) ? "Sem nome"
            : PrestadorAtual.Nome;

        public string DescPrestador => string.IsNullOrWhiteSpace(PrestadorAtual?.DescPrestador) ?
            "Sem descrição" : PrestadorAtual.DescPrestador;

        public string FotoPrestador => string.IsNullOrWhiteSpace(PrestadorAtual?.CaminhoFoto) ?
            "prestadorsemfoto.png" : PrestadorAtual.CaminhoFoto;
        public List<string> tipoCategoria => PrestadorAtual.TipoCategoria ?? new List<string>();
        private string uf = Preferences.Get("uf", string.Empty);
        public string Uf => uf;
        

    }
}
