using ConectaProApp.ViewModels.Cliente;

namespace ConectaProApp.View.Cliente;

public partial class RegisterClientTwo : ContentPage
{

    private RegisterClientViewModel _ClientViewModel;

    public RegisterClientTwo()
	{
		InitializeComponent();
        _ClientViewModel = new RegisterClientViewModel();
        BindingContext = _ClientViewModel;
    }
}