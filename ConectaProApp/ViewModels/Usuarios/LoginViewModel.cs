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
    public class LoginViewModel : BaseViewModel
    {
        public ICommand AbrirPopupCommand { get; private set; }

        public LoginViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            AbrirPopupCommand = new Command(async () => await AbrirPopup());
        }

        private async Task AbrirPopup()
        {
            try
            {
                await MopupService.Instance.PushAsync(new TipoContaAlert());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops!", ex.Message, "Ok");
            }
        }
    }
}
