namespace ConectaProApp.View.Prestador;

public partial class HomePrestador : ContentPage
{
	public HomePrestador()
	{
		InitializeComponent();
	}

	private void OnFotoPrestadorClicked(object sender, EventArgs e)
	{
		Shell.Current.FlyoutIsPresented = true;
	}
}