using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConectaProApp.Models;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Busca;
using ConectaProApp.View.Usuario;

namespace ConectaProApp.ViewModels.Prestador
{
    public class BuscaPrestadorViewModel : BaseViewModel
    {
        private readonly ServicoService _servicoService;

        // Coleção observável com os serviços encontrados
        public ObservableCollection<Servico> ServicosEncontrados { get; set; }

        // Comandos para cada categoria
        public ICommand TecnologiaSolicitacoesCommand { get; }
        public ICommand ConstrucaoSolicitacoesCommand { get; }
        public ICommand LimpezaSolicitacoesCommand { get; }
        public ICommand ReparosSolicitacoesCommand { get; }
        public ICommand JardinagemSolicitacoesCommand { get; }
        public ICommand MecanicoSolicitacoesCommand { get; }
        public ICommand PinturaSolicitacoesCommand { get; }
        public ICommand MotoristaSolicitacoesCommand { get; }

        public BuscaPrestadorViewModel()
        {
            _servicoService = new ServicoService();
            ServicosEncontrados = new ObservableCollection<Servico>();

            // Inicialização dos comandos com suas respectivas ações
            TecnologiaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Tecnologia"));
            ConstrucaoSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Construcao"));
            LimpezaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Limpeza"));
            ReparosSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Reparos"));
            JardinagemSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Jardinagem"));
            MecanicoSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Mecanico"));
            PinturaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Pintura"));
            MotoristaSolicitacoesCommand = new Command(async () => await BuscarServicosPorCategoria("Motorista"));
        }

        private async Task BuscarServicosPorCategoria(string categoria)
        {
            try
            {
                var listaServicos = await _servicoService.BuscarServicoAsync(categoria);

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
