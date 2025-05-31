using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using ConectaProApp.Models;
using ConectaProApp.ViewModels.Servico;
using ServicoModel = ConectaProApp.Models.Servico;

namespace ConectaProApp.ViewModels.Prestador
{
    [QueryProperty(nameof(ServicosOriginais), "ServicosOriginais")]
    [QueryProperty(nameof(TituloBusca), "TituloBusca")]
    public partial class ResultadoBuscaPrestadorViewModel : BaseViewModel
    {
        // O CommunityToolkit vai gerar a propriedade pública automaticamente

        [ObservableProperty]
        private List<ServicoModel> servicosOriginais;

        [ObservableProperty]
        private string tituloBusca;

        // Lista observável usada para a tela (exibida na UI)
        public ObservableCollection<ServicoViewModel> Servicos { get; } = new();

        public ResultadoBuscaPrestadorViewModel()
        {
            // Nada a fazer aqui já que a lista Servicos é inicializada diretamente
        }

        // Método parcial gerado automaticamente quando servicosOriginais muda
        partial void OnServicosOriginaisChanged(List<ServicoModel> value)
        {
            CarregarServicos();
        }

        private void CarregarServicos()
        {
            Servicos.Clear();

            if (ServicosOriginais == null)
                return;

            foreach (var servico in ServicosOriginais)
            {
                Servicos.Add(new ServicoViewModel(servico));
            }
        }
    }
}
