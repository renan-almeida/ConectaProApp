using ConectaProApp.Models;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Cliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{
    public class HomeClientViewModel: BaseViewModel
    {
        private ServicoService sService;
        public ICommand PropostaCommand { get; set; }
        public ICommand CriarServicoCommand { get; set; }
        public ICommand ProximoPrestadorCommand { get; set; }

        public HomeClientViewModel()
        {
            sService = new ServicoService();
            InitializeCommands();
            Task.Run(async () => await BuscarPrestadorAsync());
            Task.Run(async () => await CarregarFotoEmpresaAsync());
        }

        private void InitializeCommands()
        {
            ProximoPrestadorCommand = new Command(MostrarProximoPrestador);
            CriarServicoCommand = new Command(async () => await CriarSolicitacaoView());
            PropostaCommand = new Command(async () => await Shell.Current.GoToAsync("///PropostaClient"));
        }


       
        private async Task CriarSolicitacaoView()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new CriarSolicitacaoClient());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Erro", ex.Message + "Detalhes: " + ex.InnerException, "Ok");
            }
        }

        private List<Models.Prestador> prestadorUf;
        private int indice = 0;

        private Models.Prestador prestadorAtual;
        public Models.Prestador PrestadorAtual
        {
            get => prestadorAtual;
            set
            {
                prestadorAtual = value;
                OnPropertyChanged();
            }
        }

        private ImageSource fotoEmpresaUrl;
        public ImageSource FotoEmpresaUrl
        {
            get => fotoEmpresaUrl;
            set
            {
                fotoEmpresaUrl = value;
                OnPropertyChanged();
            }
        }

        private ImageSource fotoPrestadorUrl;
        public ImageSource FotoPrestadorUrl
        {
            get => fotoPrestadorUrl;
            set
            {
                fotoPrestadorUrl = value;
                OnPropertyChanged();
            }
        }

        private async Task BuscarPrestadorAsync()
        {
            var uf = Preferences.Get("UfEmpresa", string.Empty);
            prestadorUf = await sService.BuscarPrestadorUfAsync(uf);

            indice = 0;
            MostrarProximoPrestador();
        }

        private void MostrarProximoPrestador()
        {
            if (prestadorUf == null || !prestadorUf.Any())
                return;
            if (indice >= prestadorUf.Count)
                indice = 0;

            PrestadorAtual = prestadorUf[indice];
            CarregarFotoPrestador();
            indice++;
        }

        private async Task CarregarFotoPrestador()
        {
            if (PrestadorAtual?.IdUsuario != null && PrestadorAtual.CaminhoFoto != null)
            {
             /*   FotoPrestadorUrl = ImageSource.FromStream(() => new MemoryStream(PrestadorAtual.CaminhoFoto));*/
            }
            else
            {
                FotoPrestadorUrl = ImageSource.FromFile("prestadorsemfoto.png");
            }
        }

        private async Task CarregarFotoEmpresaAsync()
        {
            var fotoSalva = await SecureStorage.GetAsync("FotoEmpresa");

            if (!string.IsNullOrEmpty(fotoSalva))
            {
                FotoEmpresaUrl = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(fotoSalva)));
            }
            else
            {
                FotoEmpresaUrl = ImageSource.FromFile("empresasemfoto.png");
            }

        }
    }
}
