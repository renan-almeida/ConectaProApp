namespace ConectaProApp.View.CadastroCliente;

public partial class EtapaDois : ContentPage
{
	public EtapaDois()
	{
		InitializeComponent();
	}

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EtapaTres());
    }
}