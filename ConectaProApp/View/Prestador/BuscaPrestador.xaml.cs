using ConectaProApp.ViewModels.Prestador;

namespace ConectaProApp.View.Prestador;

public partial class BuscaPrestador : ContentPage
{
	private BuscaPrestadorViewModel _buscaPrestadorViewModel;
	public BuscaPrestador()
	{
		InitializeComponent();
		_buscaPrestadorViewModel = new BuscaPrestadorViewModel();
		BindingContext = _buscaPrestadorViewModel;
	}
}