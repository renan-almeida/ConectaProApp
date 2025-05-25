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
    public class BuscaClientViewModel: BaseViewModel
    {
        private readonly PrestadorService pService;
        public ObservableCollection<PrestadorResponseBuscaDTO> PrestadoresEncontrados { get; set; }
        public ICommand AcionarBuscaCommand { get; set; }
        public ICommand PrestadorTecnologiaCommand { get; set; }
        public ICommand PrestadorConstrucaoCommand { get; set; }
        public ICommand PrestadorLimpezaCommand { get; set; }
        public ICommand PrestadorReparosCommand { get; set; }
        public ICommand PrestadorJardinagemCommand { get; set; }
        public ICommand PrestadorMecanicoCommand { get; set; }
        public ICommand PrestadorPinturaCommand { get; set; }
        public ICommand PrestadorMotoristaCommand { get; set; }


        public BuscaClientViewModel()
        {
            pService = new PrestadorService();
            InitializeCommands();
            CarregarFotoEmpresaAsync();
            PrestadoresEncontrados = new ObservableCollection<PrestadorResponseBuscaDTO>();
        }

        private void InitializeCommands()
        {
           PrestadorTecnologiaCommand = new Command(async () => await BuscarPrestadorPorCategoria("Tecnologia"));
            PrestadorConstrucaoCommand = new Command(async () => await BuscarPrestadorPorCategoria("Construcao"));
            PrestadorLimpezaCommand = new Command(async () => await BuscarPrestadorPorCategoria("Limpeza"));
            PrestadorReparosCommand =  new Command(async () => await BuscarPrestadorPorCategoria("Reparos"));
            PrestadorJardinagemCommand = new Command(async () => await BuscarPrestadorPorCategoria("Jardinagem"));
            PrestadorMecanicoCommand = new Command(async () => await BuscarPrestadorPorCategoria("Mecanico"));
            PrestadorPinturaCommand =  new Command(async () => await BuscarPrestadorPorCategoria("Pintura"));
            PrestadorMotoristaCommand = new Command(async () => await BuscarPrestadorPorCategoria("Motorista"));
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
