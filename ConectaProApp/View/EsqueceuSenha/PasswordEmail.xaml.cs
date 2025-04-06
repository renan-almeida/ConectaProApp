namespace ConectaProApp.View.EsqueceuSenha;

public partial class PasswordEmail : ContentPage
{
	public PasswordEmail()
	{
		InitializeComponent();
        BindingContext = new EsqueceuSenhaViewModel();
    }
}