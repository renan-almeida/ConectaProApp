namespace ConectaProApp.View.EsqueceuSenha;

public partial class PasswordCode : ContentPage
{
	public PasswordCode()
	{
		InitializeComponent();
        BindingContext = new EsqueceuSenhaViewModel();
    }
}