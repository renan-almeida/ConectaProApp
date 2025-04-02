namespace ConectaProApp.View.CadastroCliente;

public partial class EtapaFinal : ContentPage
{
	public EtapaFinal()
	{
		InitializeComponent();
	}

    private async void OnCreateAccountButtonClicked(object sender, EventArgs e)
    {
        // Lógica para criar a conta
        await DisplayAlert("Conta Criada", "Sua conta foi criada com sucesso!", "OK");

        // Navegar para a página de login ou outra página
        await Navigation.PushAsync(new View.Usuario.LoginView());
    }
}