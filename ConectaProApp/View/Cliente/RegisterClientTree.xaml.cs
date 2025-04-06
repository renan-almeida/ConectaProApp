using ConectaProApp.ViewModels.Cliente;

namespace ConectaProApp.View.Cliente;

public partial class RegisterClientTree : ContentPage
{
    private RegisterClientViewModel _ClientViewModel;

    public RegisterClientTree()
	{
		InitializeComponent();
		_ClientViewModel = new RegisterClientViewModel();
		BindingContext = _ClientViewModel;
	}
}