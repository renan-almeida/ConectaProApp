using ConectaProApp.ViewModels.Cliente;
using System.Windows.Input;

namespace ConectaProApp.View.Cliente;

public partial class RegisterClient : ContentPage
{
	private RegisterClientViewModel _ClientViewModel;
	public RegisterClient()
	{
		InitializeComponent();
		_ClientViewModel = new RegisterClientViewModel();
		BindingContext = _ClientViewModel;
	}


}