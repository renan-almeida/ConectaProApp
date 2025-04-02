namespace ConectaProApp.View.CadastroPrestador;

public partial class EtapaDois_Prestador : ContentPage
{
	public EtapaDois_Prestador()
	{
		InitializeComponent();
	}

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EtapaTres_Prestador());
    }
}