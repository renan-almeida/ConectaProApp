namespace ConectaProApp.View.CadastroCliente;

public partial class EtapaUm : ContentPage
{
	public EtapaUm()
	{
		InitializeComponent();
	}

    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new EtapaDois());
    }
}