using ConectaProApp.Models;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Prestador
{
    public class HomePrestadorViewModel: BaseViewModel
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
            // busca os serviços ao inicializar a tela
            Task.Run(async () => await BuscarServicosAsync());
            Task.Run(async () => await CarregarFotoPrestadorAsync());
        }

        private List<Servico> servicosUf;
        private int indice = 0;

        private Servico servicoAtual;
        public Servico ServicoAtual
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
            var uf = Preferences.Get("UfPrestador", string.Empty);
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
            CarregarFotoEmpresa();
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

        private async Task CarregarFotoEmpresa()
        {
            if (ServicoAtual?.Cliente != null && ServicoAtual.Cliente.FotoUrl != null)
            {
                FotoEmpresaUrl = ImageSource.FromStream(() => new MemoryStream(ServicoAtual.Cliente.FotoUrl));
            }
            else
            {
                FotoEmpresaUrl = ImageSource.FromFile("empresasemfoto.png");
            }
        }
    }
}
