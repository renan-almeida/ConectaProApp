namespace ConectaProApp.View.CadastroPrestador;

public partial class EtapaTres_Prestador : ContentPage
{
	public EtapaTres_Prestador()
	{
		InitializeComponent();
	}
    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EtapaFinal_Prestador());
    }
}