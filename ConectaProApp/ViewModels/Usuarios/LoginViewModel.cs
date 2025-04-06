using ConectaProApp.PopUp;
using ConectaProApp.Services;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConectaProApp.View.EsqueceuSenha;

namespace ConectaProApp.ViewModels.Usuarios
{
    public class LoginViewModel : BaseViewModel
    {

        private UsuarioServices uService;

        public ICommand EtapaUmCommand { get; set; }
        public ICommand AbrirPopupCommand { get; set; }

        public ICommand EsqueceuSenhaCommand { get; }



        public LoginViewModel()
        {
            InitializeCommands();
            EsqueceuSenhaCommand = new Command(async () => await EsqueceuSenha());
        }
       
   

        private async Task EsqueceuSenha()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PasswordEmail());
        }

        private void InitializeCommands()
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
                await Application.Current.MainPage.DisplayAlert("Ops!", ex.Message, "Ok");
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
