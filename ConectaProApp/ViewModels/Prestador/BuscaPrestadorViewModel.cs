using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using ConectaProApp.Models;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Busca;
using ConectaProApp.View.Usuario;
using ServicoModel = ConectaProApp.Models.Servico; // Alias para a classe Servico

namespace ConectaProApp.ViewModels.Prestador
{
    public class BuscaPrestadorViewModel : BaseViewModel
    {
        private readonly ServicoService _servicoService;

        // Coleção observável com os serviços encontrados
        public ObservableCollection<ServicoModel> ServicosEncontrados { get; set; } // Usando o alias

        // Comandos para cada categoria
        public ICommand TecnologiaSolicitacoesCommand { get; set; }
        public ICommand ConstrucaoSolicitacoesCommand { get; set; }
        public ICommand LimpezaSolicitacoesCommand { get; set; }
        public ICommand MarketingSolicitacoesCommand { get; set; }
        public ICommand JardinagemSolicitacoesCommand { get; set; }
        public ICommand EsteticaSolicitacoesCommand { get; set; }
        public ICommand RefrigeracaoSolicitacoesCommand { get; set; }
        public ICommand EletricaSolicitacoesCommand { get; set; }
        public ICommand DesignSolicitacoesCommand { get; set; }
        public ICommand OutrasSolicitacoesCommand { get; set; }
        public ICommand RedesSolicitacoesCommand { get; set; }
        public ICommand TelecomunicacoesSolicitacoesCommand { get; set; }
        public ICommand SegurancaSolicitacoesCommand { get; set; }
        public ICommand MonitoramentoSolicitacoesCommand { get; set; }
        public ICommand ReparosSolicitacoesCommand { get; set; }
        public ICommand ContabilSolicitacoesCommand { get; set; }
        public ICommand BelezaSolicitacoesCommand { get; set; }
        public ICommand AutomotivoSolicitacoesCommand { get; set; }
        public ICommand AcionarBuscaCommand { get; set; }

        public BuscaPrestadorViewModel()
        {
            _servicoService = new ServicoService();
            ServicosEncontrados = new ObservableCollection<ServicoModel>();

            // Inicialização dos comandos com suas respectivas ações
            TecnologiaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("TECNOLOGIA"));
            ConstrucaoSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("CONSTRUCAO"));
            LimpezaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("LIMPEZA"));
            MarketingSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("MARKETING"));
            JardinagemSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("JARDINAGEM"));
            EsteticaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("ESTETICA"));
            RefrigeracaoSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("REFRIGERACAO"));
            EletricaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("ELETRICA"));
            DesignSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("DESIGN"));
           OutrasSolicitacoesCommand = new Command(async () =>  OutrasSolicitacoes());
            RedesSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("REDES"));
            TelecomunicacoesSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("TELECOMUNICACOES"));
            SegurancaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("SEGURANCA"));
            MonitoramentoSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("MONITORAMENTO"));
            ReparosSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("REPAROS"));
            ContabilSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("CONTABIL"));
            BelezaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("BELEZA"));
            AutomotivoSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("AUTOMOTIVO"));
            AcionarBuscaCommand = new Command(async () => await BuscarServicosPorTermo(TermoBusca));
        }

        private string termoBusca;
        public string TermoBusca
        {
            get => termoBusca;
            set
            {
                termoBusca = value;
                OnPropertyChanged();
            }
        }
        private async Task BuscarServicosPorCategoria(string categoria)
        {
            try
            {
                var listaServicos = await _servicoService.BuscarServicoPorCategoriaAsync(categoria);

                if (listaServicos != null && listaServicos.Any())
                {
                    await Shell.Current.GoToAsync(nameof(ResultadoBuscaPrestadorView),
                         new Dictionary<string, object>
                         {
                            { "Servicos", listaServicos },
                            { "TituloBusca", $"Resultados encontrados para \"{categoria}\""}
                         });
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Não encontrado", "Nenhum serviço foi encontrado", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar serviços por categoria: " + ex.Message);
            }
        }

        private async Task BuscarServicosPorTermo(string termo)
        {
            try
            {
               
                var listaServicos = await _servicoService.BuscarServicoAsync(termo);

                if (listaServicos != null && listaServicos.Any())
                {
                    await Shell.Current.GoToAsync(nameof(ResultadoBuscaPrestadorView),
                         new Dictionary<string, object>
                         {
                            { "Servicos", listaServicos },
                            { "TituloBusca", $"Resultados encontrados para \"{termo}\""}
                         });
                }
                else
                {
                    await Application.Current.MainPage
                        .DisplayAlert("Não encontrado", "Nenhum serviço foi encontrado", "OK");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar serviços por categoria: " + ex.Message);
            }

            
        }
        private async Task OutrasSolicitacoes()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Prestador.BuscaPrestadorOutros());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage
                    .DisplayAlert("Erro" + ex.Message, "Detalhes: ", ex.InnerException + "OK");
            }
        }
    }
}