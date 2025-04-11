using ConectaProApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConectaProApp.View.EsqueceuSenha;

namespace ConectaProApp.ViewModels.EsqueceuSenha
{
    public class EsqueceuSenhaViewModel : BaseViewModel
    {

        private readonly UsuarioServices _usuarioServices;

        public ICommand SolicitarCodigoCommand { get; }
        public ICommand VerificarCodigoCommand { get; }
        public ICommand RegistrarNovaSenhaCommand { get; }




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

        public EsqueceuSenhaViewModel()
        {
            _usuarioServices = new UsuarioServices();
            SolicitarCodigoCommand = new Command(async () => await SolicitarCodigo());
            VerificarCodigoCommand = new Command(async () => await VerificarCodigo());
            RegistrarNovaSenhaCommand = new Command(async () => await RegistrarNovaSenha());
        }
        private async Task SolicitarCodigo()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new PasswordCode());

        }



        private async Task VerificarCodigo()
        {
            // Lógica para verificar código
            await Application.Current.MainPage.Navigation.PushAsync(new NewPassword());

        }



        private async Task RegistrarNovaSenha()
        {
            // Lógica para registrar nova senha
        }
    }

}
