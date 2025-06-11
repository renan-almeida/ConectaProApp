using ConectaProApp.ViewModels.Prestador;

namespace ConectaProApp.View.Prestador;

public partial class BuscaPrestadorOutros : ContentPage
{
	private BuscaPrestadorViewModel buscaPrestadorViewModel;
	public BuscaPrestadorOutros()
	{
		InitializeComponent();
		buscaPrestadorViewModel = new BuscaPrestadorViewModel();
		BindingContext = buscaPrestadorViewModel;

	}

    private void OnFotoPrestadorClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");

    }
}