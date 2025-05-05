using ConectaProApp.Services.Prestador;
using ConectaProApp.View.Busca;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{
    public class BuscaClientViewModel: BaseViewModel
    {
        private PrestadorService pService;
        public ICommand AcionarBuscaCommand { get; set; }
        public ICommand PrestadorTecnologiaCommand { get; set; }
        public ICommand PrestadorConstrucaoCommand { get; set; }
        public ICommand PrestadorLimpezaCommand { get; set; }
        public ICommand PrestadorReparosCommand { get; set; }
        public ICommand PrestadorJardinagemCommand { get; set; }
        public ICommand PrestadorMecanicoCommand { get; set; }
        public ICommand PrestadorPinturaCommand { get; set; }
        public ICommand PrestadorMotoristaCommand { get; set; }


        public BuscaClientViewModel()
        {
            pService = new PrestadorService();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
           PrestadorTecnologiaCommand = new Command(async () => GetAllTecnologia());
            PrestadorConstrucaoCommand = new Command(async () => GetAllConstrucao());
            PrestadorLimpezaCommand = new Command(async () => GetAllLimpeza());
            PrestadorReparosCommand =  new Command(async () => GetAllReparos());
            PrestadorJardinagemCommand = new Command(async () => GetAllJardinagem());
            PrestadorMecanicoCommand = new Command(async () => GetAllMecanico());
            PrestadorPinturaCommand =  new Command(async () => GetAllPintura());
            PrestadorMotoristaCommand = new Command(async () => GetAllMotorista());
        }

        private async Task GetAllTecnologia()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message + "Detalhes: ", ex.InnerException + "Ok");
            }
        }

        private async Task GetAllConstrucao()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message + "Detalhes: ", ex.InnerException + "Ok");
            }
        }

        private async Task GetAllLimpeza()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message + "Detalhes: ", ex.InnerException + "Ok");
            }
        }

        private async Task GetAllReparos()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message + "Detalhes: ", ex.InnerException + "Ok");
            }
        }

        private async Task GetAllJardinagem()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message + "Detalhes: ", ex.InnerException + "Ok");
            }
        }

        private async Task GetAllMecanico()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message + "Detalhes: ", ex.InnerException + "Ok");
            }
        }

        private async Task GetAllPintura()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message + "Detalhes: ", ex.InnerException + "Ok");
            }
        }

        private async Task GetAllMotorista()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro", ex.Message + "Detalhes: ", ex.InnerException + "Ok");
            }
        }
    }
}
