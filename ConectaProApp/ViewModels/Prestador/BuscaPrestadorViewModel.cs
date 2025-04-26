using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Usuario;

namespace ConectaProApp.ViewModels.Prestador
{
    public class BuscaPrestadorViewModel
    {
        private ServicoService sService;
        
        public ICommand TecnologiaSolicitacoesCommand { get; set; }
        public ICommand ConstrucaoSolicitacoesCommand { get; set; }
        public ICommand LimpezaSolicitacoesCommand { get; set; }
        public ICommand ReparosSolicitacoesCommand { get; set; }
        public ICommand JardinagemSolicitacoesCommand { get; set; }
        public ICommand MecanicoSolicitacoesCommand { get; set; }
        public ICommand PinturaSolicitacoesCommand { get; set; }
        public ICommand MotoristaSolicitacoesCommand { get; set; }

        public BuscaPrestadorViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            TecnologiaSolicitacoesCommand = new Command(async () => GetAllTecnologia());
            ConstrucaoSolicitacoesCommand = new Command(async () => GetAllConstrucao());
            LimpezaSolicitacoesCommand = new Command(async () => GetAllLimpeza());
            ReparosSolicitacoesCommand = new Command(async () => GetAllReparos());
            JardinagemSolicitacoesCommand = new Command(async () => GetAllJardinagem());
            MecanicoSolicitacoesCommand = new Command(async () => GetAllMecanico());
            PinturaSolicitacoesCommand = new Command(async () => GetAllPintura());
            MotoristaSolicitacoesCommand = new Command(async () => GetAllMotorista());
        }

        private async Task GetAllTecnologia()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Usuario.LoginView());
            }
            catch (Exception ex)
            {

                await Application.Current.MainPage.DisplayAlert("Erro",  ex.Message + "Detalhes: ", ex.InnerException + "Ok");
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
