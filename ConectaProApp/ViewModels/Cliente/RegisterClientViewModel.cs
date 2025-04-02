using ConectaProApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{

    class RegisterClientViewModel
    {
        private UsuarioServices uService;
        public ICommand EtapaDoisRegisterClientCommand { get; set; }
        public RegisterClientViewModel()
        {
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            EtapaDoisRegisterClientCommand = new Command(async () => await EtapaDois());
        }

        public async Task EtapaDois()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new View.Cliente.RegisterClientTwo());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Opss!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }

        }
    }
}
