using ConectaProApp.Services.Prestador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Prestador
{
    public class RegisterPrestadorViewModel : BaseViewModel
    {
        private PrestadorService pService;

        public ICommand EtapaDoisRegisterPrestadorCommand { get; set; }
        public ICommand EtapaTresRegisterPrestadorCommand { get; set; }
        public ICommand EtapaQuatroRegisterPrestadorCommand { get; set; }
        public RegisterPrestadorViewModel()
        {
            pService = new PrestadorService();
            InitializeCommands();
           
        }

        public void InitializeCommands()
        {
            EtapaDoisRegisterPrestadorCommand = new Command(async () => EtapaDois());
            EtapaTresRegisterPrestadorCommand = new Command(async () => EtapaTres());
            EtapaQuatroRegisterPrestadorCommand = new Command(async () => EtapaQuatro());
        }

        public async Task EtapaDois()
        {
            try
            {
                var proximaPagina = new View.Prestador.RegisterPrestadorTwo
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Opss!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }

        public async Task EtapaTres()
        {
            try
            {
                var proximaPagina = new View.Prestador.RegisterPrestadortree
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Opss!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }

        public async Task EtapaQuatro()
        {
            try
            {
                var proximaPagina = new View.Prestador.RegisterPrestadorFinal
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Opss!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }

    }
}
