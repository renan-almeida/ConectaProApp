using ConectaProApp.Models;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Prestador;
using ConectaProApp.ViewModels.Servico;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ServicoModel = ConectaProApp.Models.Servico; // Alias para a classe Servico

namespace ConectaProApp.ViewModels.Prestador
{
    public class HomePrestadorViewModel : BaseViewModel
    {
        private ServicoService sService;

        public ICommand BuscarServicosCommand { get; set; }
        public ICommand ProximoServicoCommand { get; set; }
        public ICommand CandidatarServicoCommand { get; set; }

        public HomePrestadorViewModel()
        {
            sService = new ServicoService();
            BuscarServicosCommand = new Command(async () => await BuscarServicosAsync());
            ProximoServicoCommand = new Command(MostrarProximoServico);
            CandidatarServicoCommand = new Command(async () =>
            {
                var idSolicitacao = ServicoAtual?.IdSolicitacao ?? 0;
                if (idSolicitacao > 0)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CandidatarPrestador(idSolicitacao));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Nenhum serviço selecionado.", "OK");
                }
            });
            // busca os serviços ao inicializar a tela
            Task.Run(async () => await BuscarServicosAsync());
            Task.Run(async () => await CarregarFotoPrestadorAsync());
        }

        private List<ServicoHomeDTO> servicosUf; // Usando o alias ServicoModel
        private int indice = 0;
        /*
                private ServicoModel servicoAtual; // Usando o alias ServicoModel
                public ServicoModel ServicoAtual
                {
                    get => servicoAtual;
                    set
                    {
                        servicoAtual = value;
                        OnPropertyChanged();
                    }
                }
        */

        private ServicoHomeDTO servicoAtual;
        public ServicoHomeDTO ServicoAtual
        {
            get => servicoAtual;
            set
            {
                servicoAtual = value;
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

        private async Task BuscarServicosAsync()
        {
            var uf = Preferences.Get("uf", string.Empty);
            servicosUf = await sService.BuscarServicoUfAsync(uf);

            indice = 0;
            MostrarProximoServico();
        }

        private void MostrarProximoServico()
        {
            if (servicosUf == null || !servicosUf.Any())
                return;

            if (indice >= servicosUf.Count)
                indice = 0;

            ServicoAtual = servicosUf[indice];
            indice++;
        }

        private async Task CarregarFotoPrestadorAsync()
        {
            var fotoSalva = await SecureStorage.GetAsync("FotoPrestador");

            if (!string.IsNullOrEmpty(fotoSalva))
                FotoPrestadorUrl = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(fotoSalva)));
            else
                FotoPrestadorUrl = ImageSource.FromFile("prestadorsemfoto.png");
        }
    }
}