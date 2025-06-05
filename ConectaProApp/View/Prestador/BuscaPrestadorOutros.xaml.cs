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
}