using ConectaProApp.ViewModels.Cliente;

namespace ConectaProApp.View.Cliente;

public partial class BuscaClient : ContentPage
{
	private BuscaClientViewModel _buscaClientViewModel;
	public BuscaClient()
	{
		InitializeComponent();
		_buscaClientViewModel = new BuscaClientViewModel();
		BindingContext = _buscaClientViewModel;
	}

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}