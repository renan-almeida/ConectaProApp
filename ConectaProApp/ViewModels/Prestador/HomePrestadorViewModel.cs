using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.ViewModels.Prestador
{
    public class HomePrestadorViewModel: BaseViewModel
    {
        private PrestadorService pService;
        private ServicoService sService;
        public HomePrestadorViewModel()
        {
            pService = new PrestadorService();
            sService = new ServicoService();
        }

        public ObservableCollection<Models.Servico> Solicitacoes { get; set; }

        public async Task CarregarSolicitacoesAsync()
        {
            try
            {
                var ufPrestador = Preferences.Get("UfPrestador", string.Empty);
                var resultado = await sService.BuscarServicoUfAsync(ufPrestador);

                Solicitacoes = new ObservableCollection<Models.Servico>(resultado);
            }
            catch ( Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message, "Ok");
               
            }
        }
    }
}
