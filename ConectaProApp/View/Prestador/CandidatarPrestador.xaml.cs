namespace ConectaProApp.View.Prestador;

public partial class CandidatarPrestador : ContentPage
{
	public CandidatarPrestador()
	{
		InitializeComponent();
	}

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        // Navega para a página anterior
        await Shell.Current.GoToAsync("///prestador");
    }

    private async void OnFotoPrestadorClicked(object sender, EventArgs e)
    {
        // Exemplo de ação ao clicar na foto do prestador
        await Application.Current.MainPage.DisplayAlert("Foto do Prestador", "Você clicou na foto do prestador.", "OK");
    }

    private async void OnAvatarPrestadorTapped(object sender, EventArgs e)
    {
        // Exemplo de ação ao clicar na imagem do prestador
        if (sender is Image image)
        {
            // Realiza o zoom (aumenta o tamanho da imagem)
            await image.ScaleTo(1.5, 500, Easing.CubicInOut);

            // Opcional: Retorna ao tamanho original após um pequeno atraso
            await Task.Delay(500);
            await image.ScaleTo(1.0, 500, Easing.CubicInOut);
        }
    }
}