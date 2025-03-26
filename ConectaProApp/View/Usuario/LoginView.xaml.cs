using System.Threading.Tasks;

namespace ConectaProApp.View.Usuario;

public partial class LoginView : ContentPage
{
	public LoginView()
	{
		InitializeComponent();
	}

	private async Task OnLabelTapped(object sender, EventArgs e)
	{
		string action = await DisplayActionSheet("Qual tipo de Conta deseja criar?", "Cancelar", null, "Cliente", "Prestador");

        if (action == "Cliente")
        {
            await Shell.Current.GoToAsync("RegistroClienteView");
        }
        else if (action == "Prestador")
        {
            await Shell.Current.GoToAsync("RegistroPrestadorView");
        }

    }
}