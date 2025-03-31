using ConectaProApp.ViewModels.Usuarios;

namespace ConectaProApp
{
    public partial class MainPage : ContentPage
    {
        UsuarioViewModel _usuarioViewModel;
        public MainPage()
        {
            InitializeComponent();

            _usuarioViewModel = new UsuarioViewModel();
            BindingContext = _usuarioViewModel;
        }

    }

}
