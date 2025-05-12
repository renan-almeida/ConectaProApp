namespace ConectaProApp.View.Cliente;

public partial class PropostaClient : ContentPage
{
	public PropostaClient()
	{
		InitializeComponent();
	}

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        // Navega para a p�gina anterior
        await Shell.Current.GoToAsync("///cliente");
    }

    private async void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        // Exemplo de a��o ao clicar na foto da empresa
        await Application.Current.MainPage.DisplayAlert("Foto da Empresa", "Voc� clicou na foto da empresa.", "OK");
    }

    private async void OnAvatarClientTapped(object sender, EventArgs e)
    {
        // Verifica se o sender � uma imagem
        if (sender is Image image)
        {
            // Realiza o zoom (aumenta o tamanho)
            await image.ScaleTo(1.5, 500, Easing.CubicInOut);

            // Opcional: Retorna ao tamanho original ap�s um pequeno atraso
            await Task.Delay(500);
            await image.ScaleTo(1.0, 250, Easing.CubicInOut);
        }
    }
}