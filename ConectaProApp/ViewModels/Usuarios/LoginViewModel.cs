using ConectaProApp.PopUp;
using ConectaProApp.Services;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Usuarios
{
    class LoginViewModel : BaseViewModel
    {
        private UsuarioServices uService;

        public ICommand EtapaUmCommand { get; set; }
        public ICommand AbrirPopupCommand { get; set; } 
        

        public LoginViewModel()
        {
            uService = new UsuarioServices();
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            EtapaUmCommand = new Command(async () => await EtapaUm());
            AbrirPopupCommand = new Command(async () => await AbrirPopup());
        }

        public async Task EtapaUm()
        {
            try
            {
                await MopupService.Instance.PopAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new View.Cliente.RegisterClient());

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                   ("Ops!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }

        public async Task AbrirPopup()
        {
            try
            {
                await Mopups.Services.MopupService.Instance.PushAsync(new PopUp.TipoContaAlert());

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                   ("Ops!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }
    
    }
}
