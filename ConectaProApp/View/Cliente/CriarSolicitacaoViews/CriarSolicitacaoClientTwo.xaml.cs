using ConectaProApp.ViewModels.Cliente;

namespace ConectaProApp.View.Cliente.CriarSolicitacaoViews;

public partial class CriarSolicitacaoClientTwo : ContentPage
{
	private CriarSolicitacaoViewModel _criarSolicitacaoViewModel;
	public CriarSolicitacaoClientTwo()
	{
		InitializeComponent();
		_criarSolicitacaoViewModel = new CriarSolicitacaoViewModel();
		BindingContext = _criarSolicitacaoViewModel;
	}

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new CriarSolicitacaoClient());
    }
}