using ConectaProApp.ViewModels.EsqueceuSenha;
namespace ConectaProApp.View.EsqueceuSenha;

public partial class NewPassword : ContentPage
{
	public NewPassword()
	{
		InitializeComponent();
        BindingContext = new EsqueceuSenhaViewModel();
    }
}