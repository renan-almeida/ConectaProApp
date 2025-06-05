using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using ConectaProApp.Models;
using ConectaProApp.Services;
using ConectaProApp.Services.Cliente;

namespace ConectaProApp.ViewModels.Cliente
{
    public class PerfilEmpresaViewModel : BaseViewModel
    {
        private readonly PerfilEmpresaClienteService peService;

        private ObservableCollection<ServicoDTO> historico;

        public ObservableCollection<ServicoDTO> Historico
        {
            get => historico;
            set
            {
                historico = value;
                OnPropertyChanged();
            }
        }

        private bool historicoClienteVisivel;
        public bool HistoricoClienteVisivel
        {
            get => historicoClienteVisivel;
            set
            {
                historicoClienteVisivel = value;
                OnPropertyChanged();
            }
        }

        public int IdEmpresa { get; set; }

        // Recebe ApiService no construtor para injetar no serviço
        public PerfilEmpresaViewModel(int idEmpresa, ApiService apiService)
        {
            IdEmpresa = idEmpresa;
            peService = new PerfilEmpresaClienteService(apiService);
            Historico = new ObservableCollection<ServicoDTO>();
            HistoricoClienteVisivel = false;

            
        }

        public async Task ExibirHistorico()
        {
            try
            {
                var response = await peService.BuscarHistoricoAsync(IdEmpresa);

                if (response != null && response.Count > 0)
                {
                    Historico = new ObservableCollection<ServicoDTO>(response);
                    HistoricoClienteVisivel = true;
                }
                else
                {
                    HistoricoClienteVisivel = false;
                }
            }
            catch (Exception ex)
            {
                // Aqui pode logar ou tratar melhor o erro, mostrar mensagem etc
                HistoricoClienteVisivel = false;
                throw new Exception("Erro ao buscar serviços históricos.", ex);
            }
        }
    }
}
