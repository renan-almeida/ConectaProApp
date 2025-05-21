using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ConectaProApp.ViewModels.Servico;
using ServicoModel = ConectaProApp.Models.Servico;

namespace ConectaProApp.ViewModels.Prestador
{
    public class ResultadoBuscaPrestadorViewModel : BaseViewModel
    {
        public ObservableCollection<ServicoViewModel> Servicos { get; set; }

        public ResultadoBuscaPrestadorViewModel(List<ServicoModel> servicos)
        {
            Servicos = new ObservableCollection<ServicoViewModel>(
                    (servicos ?? new List<ServicoModel>()).Select(s => new ServicoViewModel(s))
    );
        }
    }
}
