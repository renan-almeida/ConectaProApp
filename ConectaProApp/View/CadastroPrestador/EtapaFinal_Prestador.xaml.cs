namespace ConectaProApp.View.CadastroPrestador;

public partial class EtapaFinal_Prestador : ContentPage
{
	public EtapaFinal_Prestador()
	{
		InitializeComponent();
	}

    private async void OnCreateAccountButtonClicked(object sender, EventArgs e)
    {
        // Ação final, como salvar os dados e navegar para a página inicial ou de confirmação
        await DisplayAlert("Conta Criada", "Sua conta foi criada com sucesso!", "OK");
        // Navegar para a página inicial ou de confirmação
        await Navigation.PopToRootAsync();
    }
}