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

        public ICommand AbrirPopupCommand { get; set; }

        public LoginViewModel()
        {
            uService = new UsuarioServices();
            InitializeCommands();
        }

        public void InitializeCommands()
        {
            AbrirPopupCommand = new Command(async () => await AbrirPopup());
        }

        public async Task AbrirPopup()
        {
            try
            {
                await MopupService.Instance.PushAsync(new TipoContaAlert());
         
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                   ("Ops!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }
    }
}
