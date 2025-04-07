using ConectaProApp.ViewModels.Prestador;

namespace ConectaProApp.View.Prestador;

public partial class RegisterPrestadorTwo : ContentPage
{

	private RegisterPrestadorViewModel _registerPrestador;
	public RegisterPrestadorTwo()
	{
		InitializeComponent();
		_registerPrestador = new RegisterPrestadorViewModel();
		BindingContext = _registerPrestador;

	}
}