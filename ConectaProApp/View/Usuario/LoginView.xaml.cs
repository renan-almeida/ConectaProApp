
using ConectaProApp.PopUp;
using ConectaProApp.ViewModels.Usuarios;
using Mopups.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.View.Usuario;

public partial class LoginView : ContentPage
{

    LoginViewModel _loginViewModel;
    public LoginView()
    {
        InitializeComponent();
        InitializeCommands();

    }
    public ICommand AbrirPopupCommand { get; set; }
    public void InitializeCommands()
    {
        AbrirPopupCommand = new Command(async () => await AbrirPopup());
    }

    public async Task AbrirPopup()
    {
        try
        {
            string action = await DisplayActionSheet("Qual tipo de Conta deseja criar?", "Cancelar",
                null, "Cliente", "Prestador");

            if (action == "Cliente")
            {
                await Shell.Current.GoToAsync("RegistroClienteView");
            }
            else if (action == "Prestador")
            {
                await Shell.Current.GoToAsync("RegistroPrestadorView");
            }


        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert
               ("Ops!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

        }
    }
}



    

