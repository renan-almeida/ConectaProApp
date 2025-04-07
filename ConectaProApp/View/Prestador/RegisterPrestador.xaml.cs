using ConectaProApp.ViewModels.Prestador;
using ConectaProApp.ViewModels.Usuarios;

namespace ConectaProApp.View.Prestador;

public partial class RegisterPrestador : ContentPage
{
	private RegisterPrestadorViewModel _prestadorViewModel;
	public RegisterPrestador()
	{
		InitializeComponent();
		_prestadorViewModel = new RegisterPrestadorViewModel();
		BindingContext = _prestadorViewModel;
	}
}