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

        private UsuarioServices uService;

        public ICommand EtapaUmCommand { get; set; }
        public ICommand EtapaUmPrestadorCommand { get; set; }
        public ICommand AbrirPopupCommand { get; set; }

        private string email;
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string senha;
        public string Senha
        {
            get => senha;
            set
            {
                senha = value;
                OnPropertyChanged();
            }
        }


        public LoginViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            EtapaUmCommand = new Command(async () => await EtapaUm());
            AbrirPopupCommand = new Command(async () => await AbrirPopup());
            EtapaUmPrestadorCommand = new Command(async() => await EtapaUmPrestador());
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

        public async Task EtapaUmPrestador()
        {
            try
            {
                await MopupService.Instance.PopAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new View.Prestador.RegisterPrestador());


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
