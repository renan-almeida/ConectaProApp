namespace ConectaProApp.View.CadastroPrestador;

public partial class EtapaUm_Prestador : ContentPage
{
	public EtapaUm_Prestador()
	{
		InitializeComponent();
	}

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EtapaDois_Prestador());
    }
}