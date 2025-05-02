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
using ConectaProApp.Models;
using System.ComponentModel.Design;
using ConectaProApp.Models.Enuns;

namespace ConectaProApp.ViewModels.Usuarios
{
    public class LoginViewModel : BaseViewModel
    {

        private UsuarioServices uService;

        public ICommand EsqueceuSenhaCommand { get; set; }
        public ICommand EtapaUmCommand { get; set; }
        public ICommand EtapaUmPrestadorCommand { get; set; }
        public ICommand AbrirPopupCommand { get; set; }
        public ICommand EntrarCommand { get; set; }

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
            EsqueceuSenhaCommand = new Command(async () => await NavegarParaPasswordEmail());
            EntrarCommand = new Command(async () => await Entrar());
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

        public async Task Entrar()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Senha))
                {
                    await Application.Current.MainPage.DisplayAlert("Atenção", "Preencha todos os campos.", "Ok");
                    return;
                }

                var usuario = new Usuario
                {
                    Email = this.Email,
                    Senha = this.Senha
                };

                var usuarioAutenticado = await uService.PostAutenticarUsuarioAsync(usuario);

                if (usuarioAutenticado != null && usuarioAutenticado.IdUsuario > 0)
                {

                    Preferences.Set("UsuarioToken", usuarioAutenticado.Token);
                    Preferences.Set("TipoUsuario", usuarioAutenticado.TipoUsuario.ToString());
                    Preferences.Set("UfPrestador", usuarioAutenticado.Uf.ToString());
                  
                    await Application.Current.MainPage.DisplayAlert("Bem-vindo!", $"Olá, {usuarioAutenticado.Nome}!", "Ok");

                 switch (usuarioAutenticado.TipoUsuario)
                    {
                        case TipoUsuarioEnum.EMPRESA:
                            await Application.Current.MainPage.Navigation.PushAsync(new View.Cliente.HomeClient());
                            break;

                        case TipoUsuarioEnum.PRESTADOR:
                            await Application.Current.MainPage.Navigation.PushAsync(new View.Prestador.HomePrestador());
                            break;

                        default:
                            await Application.Current.MainPage.DisplayAlert("Erro", "Tipo de Usuario não encontrado", "Ok");
                            break;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Email ou senha inválidos.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Ops!", ex.Message, "Ok");
            }
        }

           
        
        private async Task NavegarParaPasswordEmail()
        {
            // Navega para a página PasswordEmail
            await Application.Current.MainPage.Navigation.PushAsync(new PasswordEmail());
        }



    }
}
