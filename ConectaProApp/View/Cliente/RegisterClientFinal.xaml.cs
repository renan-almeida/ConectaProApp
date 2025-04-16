using ConectaProApp.View.FeedingCliente;
namespace ConectaProApp.View.Cliente;

public partial class RegisterClientFinal : ContentPage
{
	public RegisterClientFinal()
	{
		InitializeComponent();
	}

    private async void OnCriarContaClicked(object sender, EventArgs e)
    {
        // Navegar para a tela FeedinClient
        await Shell.Current.GoToAsync(nameof(FeedinClient));
    }
}