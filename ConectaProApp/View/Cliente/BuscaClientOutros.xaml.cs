using ConectaProApp.ViewModels.Cliente;

namespace ConectaProApp.View.Cliente;

public partial class BuscaClientOutros : ContentPage
{
    private BuscaClientViewModel _buscaClientViewModel;
    public BuscaClientOutros()
	{
		InitializeComponent();
        _buscaClientViewModel = new BuscaClientViewModel();
        BindingContext = _buscaClientViewModel;
    }

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private void OnVoltarClicked(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }
}