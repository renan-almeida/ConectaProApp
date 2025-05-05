using ConectaProApp.Services.Servico;
using ConectaProApp.View.Cliente.CriarSolicitacaoViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{
    public class CriarSolicitacaoViewModel: BaseViewModel
    {
        private ServicoService sService;
        public ICommand EtapaDoisCommand { get; set; }
        public ICommand EtapaFinalCommand { get; set; }

        public CriarSolicitacaoViewModel()
        {
            sService = new ServicoService();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            EtapaDoisCommand = new Command(async() => await EtapaDois());
            EtapaFinalCommand = new Command(async() =>  await EtapaFinal());
        }

        public async Task EtapaDois()
        {
            try
            {
                var proximaPagina = new CriarSolicitacaoClientTwo
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Erro!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }

        public async Task EtapaFinal()
        {
            try
            {
                var proximaPagina = new CriarSolicitacaoClientFinal
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Erro!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }
    }
}
