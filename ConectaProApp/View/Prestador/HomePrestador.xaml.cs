using ConectaProApp.ViewModels.Prestador;

namespace ConectaProApp.View.Prestador;

public partial class HomePrestador : ContentPage
{
	private HomePrestadorViewModel _homePrestadorViewModel;
	public HomePrestador()
	{
		InitializeComponent();
		_homePrestadorViewModel = new HomePrestadorViewModel();
		BindingContext = _homePrestadorViewModel;
	}

	private void OnFotoPrestadorClicked(object sender, EventArgs e)
	{
		Shell.Current.FlyoutIsPresented = true;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _homePrestadorViewModel.InitAsync();
    }

}