using ConectaProApp.ViewModels.Usuarios;

namespace ConectaProApp
{
    public partial class MainPage : ContentPage
    {
        UsuarioViewModel _usuarioViewModel;
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            _usuarioViewModel = new UsuarioViewModel();
            BindingContext = _usuarioViewModel;
        }

    }
}

