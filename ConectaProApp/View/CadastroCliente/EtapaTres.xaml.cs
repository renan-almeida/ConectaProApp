namespace ConectaProApp.View.CadastroCliente;

public partial class EtapaTres : ContentPage
{
	public EtapaTres()
	{
		InitializeComponent();
	}

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EtapaFinal());
    }
}