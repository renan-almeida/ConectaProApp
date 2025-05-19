using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        public ICommand ReparosSolicitacoesCommand { get; set; }
        public ICommand JardinagemSolicitacoesCommand { get; set; }
        public ICommand MecanicoSolicitacoesCommand { get; set; }
        public ICommand PinturaSolicitacoesCommand { get; set; }
        public ICommand MotoristaSolicitacoesCommand { get; set; }
        public ICommand AcionarBuscaCommand { get; set; }

        public BuscaPrestadorViewModel()
        {
            _servicoService = new ServicoService();
            ServicosEncontrados = new ObservableCollection<ServicoModel>();

            // Inicialização dos comandos com suas respectivas ações
            TecnologiaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Tecnologia"));
            ConstrucaoSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Construcao"));
            LimpezaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Limpeza"));
            ReparosSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Reparos"));
            JardinagemSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Jardinagem"));
            MecanicoSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Mecanico"));
            PinturaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Pintura"));
            MotoristaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Motorista"));
            AcionarBuscaCommand = new Command(async () => await BuscarServicosPorTermo(TermoBusca));        }

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

                if(listaServicos == null || !listaServicos.Any())
                {
                    await App.Current.MainPage.DisplayAlert("Aviso", "Nenhum serviço encontrado.", "OK");
                    return;
                }

                if (listaServicos != null)
                {
                    ServicosEncontrados.Clear();
                    foreach (var servico in listaServicos)
                        ServicosEncontrados.Add(servico);

                    // (Opcional) Redireciona para uma página que exibe os resultados
                    await Shell.Current.GoToAsync(nameof(ResultadoBuscaPrestadorView),
                        new Dictionary<string, object> { { "Servicos", listaServicos } });
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

                if (listaServicos != null)
                {
                    ServicosEncontrados.Clear();
                    foreach (var servico in listaServicos)
                        ServicosEncontrados.Add(servico);

                    // (Opcional) Redireciona para uma página que exibe os resultados
                    await Shell.Current.GoToAsync(nameof(ResultadoBuscaPrestadorView),
                        new Dictionary<string, object> { { "Servicos", listaServicos } });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao buscar serviços por categoria: " + ex.Message);
            }
        }
    }
}