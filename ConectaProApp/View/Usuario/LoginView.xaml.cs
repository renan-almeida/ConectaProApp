
using ConectaProApp.PopUp;
using ConectaProApp.ViewModels.Usuarios;
using Mopups.Services;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.View.Usuario;

public partial class LoginView : ContentPage
{

    private LoginViewModel _loginViewModel;
    public LoginView()
    {
        InitializeComponent();
        _loginViewModel = new LoginViewModel();
        BindingContext = _loginViewModel;

    }

   
}



    

