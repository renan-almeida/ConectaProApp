using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using ConectaProApp.ViewModels;
using ConectaProApp.Services.Cliente;
using ConectaProApp.Services.Servico;
using System.Collections.ObjectModel;
using ConectaProApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;



namespace ConectaPro.Controllers
{
   
    public partial class PerfilEmpresaViewModel : BaseViewModel
    {
        private readonly PerfilEmpresaClienteService peService;

        public int IdEmpresa { get; set; }

        private ObservableCollection<ServicoDTO> historico;

        [ObservableProperty]
        private bool historicoClienteVisivel;
        public ObservableCollection<ServicoDTO> Historico
        {
            get => historico;
            set
            {
                historico = value;
                OnPropertyChanged();
            }
        }

        public PerfilEmpresaViewModel(int idEmpresa)
        {
            peService = new PerfilEmpresaClienteService();
            IdEmpresa = idEmpresa;
            Historico = new ObservableCollection<ServicoDTO>();
            _ = ExibirHistorico();
        }

        public async Task ExibirHistorico()
        {
            try
            {
                var response = await peService.BuscarHistoricoAsync(IdEmpresa);
                if (response != null)
                {
                    Historico = new ObservableCollection<ServicoDTO>(response);
                }
            }
            catch
            {
                // Trate o erro apropriadamente, como exibir um alerta ao usuário
                throw new Exception("Erro ao buscar serviços históricos.");
            }
        }
    }
}
