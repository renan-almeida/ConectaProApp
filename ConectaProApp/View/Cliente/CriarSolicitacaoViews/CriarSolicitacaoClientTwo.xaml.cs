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
        pickerPrevisaoInicio.MinimumDate = DateTime.Today;
        pickerPrevisaoInicio.MaximumDate = DateTime.Today.AddDays(365);
        pickerPrevisaoInicio.Date = DateTime.Today;

    }

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");

    }

    

}
