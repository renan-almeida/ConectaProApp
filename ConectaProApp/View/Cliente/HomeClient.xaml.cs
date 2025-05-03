using ConectaProApp.ViewModels.Cliente;

namespace ConectaProApp.View.Cliente;

public partial class HomeClient : ContentPage
{
	private HomeClientViewModel _homeClientViewModel;
	public HomeClient()
	{
		InitializeComponent();
		_homeClientViewModel = new HomeClientViewModel();
		BindingContext = _homeClientViewModel;
	}

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}