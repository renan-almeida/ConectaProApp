using ConectaProApp.PopUp;
using Mopups.Services;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Usuarios
{
    class LoginViewModel : BaseViewModel
    {
        public ICommand AbrirPopupCommand { get; set; }

        public LoginViewModel()
        {
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
