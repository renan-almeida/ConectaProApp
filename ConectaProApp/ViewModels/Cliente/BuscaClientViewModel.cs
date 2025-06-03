
using ConectaProApp.Models;
using ConectaProApp.Services.Prestador;
using ConectaProApp.View.Busca;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{
    public class BuscaClientViewModel : BaseViewModel
    {
        private readonly PrestadorService pService;
        public ObservableCollection<PrestadorResponseBuscaDTO> PrestadoresEncontrados { get; set; }
        public ICommand AcionarBuscaCommand { get; set; }
        public ICommand PrestadorTecnologiaCommand { get; set; }
        public ICommand PrestadorConstrucaoCommand { get; set; }
        public ICommand PrestadorLimpezaCommand { get; set; }
        public ICommand PrestadorMarketingCommand { get; set; }
        public ICommand PrestadorJardinagemCommand { get; set; }
        public ICommand PrestadorEsteticaCommand { get; set; }
        public ICommand PrestadorRefrigeracaoCommand { get; set; }
        public ICommand PrestadorEletricaCommand { get; set; }
        public ICommand PrestadorDesignCommand { get; set; }
        public ICommand PrestadorOutrosCommand { get; set; }
        public ICommand PrestadorRedesCommand { get; set; }
        public ICommand PrestadorTelecomunicacoesCommand { get; set; }
        public ICommand PrestadorSegurancaCommand { get; set; }
        public ICommand PrestadorMonitoramentoCommand { get; set; }
        public ICommand PrestadorReparosCommand { get; set; }
        public ICommand PrestadorContabilCommand { get; set; }
        public ICommand PrestadorBelezaCommand { get; set; }
        public ICommand PrestadorAutomotivoCommand { get; set; }
        public BuscaClientViewModel()
        {
            pService = new PrestadorService();
            InitializeCommands();
            CarregarFotoEmpresaAsync();
            PrestadoresEncontrados = new ObservableCollection<PrestadorResponseBuscaDTO>();
        }

        private void InitializeCommands()
        {
            PrestadorTecnologiaCommand = new Command(async () => await BuscarPrestadorPorCategoria("TECNOLOGIA"));
            PrestadorConstrucaoCommand = new Command(async () => await BuscarPrestadorPorCategoria("CONSTRUCAO"));
            PrestadorLimpezaCommand = new Command(async () => await BuscarPrestadorPorCategoria("LIMPEZA"));
            PrestadorMarketingCommand = new Command(async () => await BuscarPrestadorPorCategoria("MARKETING"));
            PrestadorJardinagemCommand = new Command(async () => await BuscarPrestadorPorCategoria("JARDINAGEM"));
            PrestadorEsteticaCommand = new Command(async () => await BuscarPrestadorPorCategoria("ESTETICA"));
            PrestadorRefrigeracaoCommand = new Command(async () => await BuscarPrestadorPorCategoria("REFRIGERACAO"));
            PrestadorEletricaCommand = new Command(async () => await BuscarPrestadorPorCategoria("ELETRICA"));
            PrestadorDesignCommand = new Command(async () => await BuscarPrestadorPorCategoria("DESIGN"));
            PrestadorOutrosCommand = new Command(async () => await BuscarOutrasCategorias());
            PrestadorRedesCommand = new Command(async () => await BuscarPrestadorPorCategoria("REDES"));
            PrestadorTelecomunicacoesCommand = new Command(async () => await BuscarPrestadorPorCategoria("TELECOMUNICACOES"));
            PrestadorSegurancaCommand = new Command(async () => await BuscarPrestadorPorCategoria("SEGURANÇA"));
            PrestadorMonitoramentoCommand = new Command(async () => await BuscarPrestadorPorCategoria("MONITORAMENTO"));
            PrestadorReparosCommand = new Command(async () => await BuscarPrestadorPorCategoria("REPAROS"));
            PrestadorContabilCommand = new Command(async () => await BuscarPrestadorPorCategoria("CONTABIL"));
            PrestadorBelezaCommand = new Command(async () => await BuscarPrestadorPorCategoria("BELEZA"));
            PrestadorAutomotivoCommand = new Command(async () => await BuscarPrestadorPorCategoria("AUTOMOTIVO"));
            AcionarBuscaCommand = new Command(async () => await BuscarPrestadorPorTermo(TermoDigitado));
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

        private string termoDigitado;
        public string TermoDigitado
        {
            get => termoDigitado;
            set
            {
                termoDigitado = value;
                OnPropertyChanged();
            }
        }

        private async Task BuscarPrestadorPorCategoria(string categoria)
        {
            try
            {
                var listaPrestadores = await pService.BuscarPrestadorPorCategoriaAsync(categoria);

                if (listaPrestadores != null)
                {
                    PrestadoresEncontrados.Clear();
                    foreach (var prestador in listaPrestadores)
                        PrestadoresEncontrados.Add(prestador);

                    await Shell.Current.GoToAsync(nameof(ResultadoBuscaClientView),
                        new Dictionary<string, object>
                        {
                            { "Prestadores", listaPrestadores },
                            { "TituloBusca", $"Prestadores encontrados para \"{categoria}\""}
                        });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar Prestadores: " + ex.Message);
            }
        }
        private async Task BuscarPrestadorPorTermo(string termo)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(termo))
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Aviso",
                        "Digite o nome ou categoria do prestador que deseja buscar",
                        "OK");
                }

                var listaPrestadores = await pService.BuscarPrestadorAsync(termo);

                if (listaPrestadores != null)
                {
                    PrestadoresEncontrados.Clear();
                    foreach (var prestador in listaPrestadores)
                        PrestadoresEncontrados.Add(prestador);

                    // (Opcional) Redireciona para uma página que exibe os resultados
                    await Shell.Current.GoToAsync(nameof(ResultadoBuscaClientView),
                        new Dictionary<string, object>
                        {
                            { "Prestadores", listaPrestadores },
                            { "TituloBusca", $"Resultados encontrados para \"{termo}\""}
                        });

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar Prestadores: " + ex.Message);
            }
        }
        private async Task CarregarFotoEmpresaAsync()
        {
   
                FotoEmpresaUrl = "empresasemfoto.png";

        }

        private async Task BuscarOutrasCategorias()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Cliente.BuscaClientOutros());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage
                    .DisplayAlert("Erro", ex.Message, "Detalhes: " + ex.InnerException + "OK");
            }
        }
    }
}
