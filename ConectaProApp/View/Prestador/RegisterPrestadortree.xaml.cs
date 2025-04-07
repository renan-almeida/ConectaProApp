using ConectaProApp.ViewModels.Prestador;

namespace ConectaProApp.View.Prestador;

public partial class RegisterPrestadortree : ContentPage
{
	private RegisterPrestadorViewModel _prestadorViewModel;
	public RegisterPrestadortree()
	{
		InitializeComponent();
		_prestadorViewModel = new RegisterPrestadorViewModel();
		BindingContext = _prestadorViewModel;
	}
}