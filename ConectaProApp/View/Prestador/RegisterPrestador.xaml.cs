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
        dtNascimentoDatePicker.MinimumDate = new DateTime(1960,1,1);
        dtNascimentoDatePicker.MaximumDate = DateTime.Today.AddYears(-18) ;
    }
}