using ConectaProApp.ViewModels.Prestador;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using prestadorModel = ConectaProApp.Models.Prestador;
namespace ConectaProApp.ViewModels.Cliente
{
    public class ResultadoBuscaClientViewModel: BaseViewModel
    {
        public ObservableCollection<PrestadorViewModel> Prestadores { get; set; }

        public ResultadoBuscaClientViewModel(List<prestadorModel> prestadores)
        {
            Prestadores = new ObservableCollection<PrestadorViewModel>(
                prestadores.Select(p => new PrestadorViewModel(p))
            );
        }

     
    }
}
