namespace ConectaProApp.View.Cliente;

public partial class CriarSolicitacaoClient : ContentPage
{
	public CriarSolicitacaoClient()
	{
		InitializeComponent();
	}

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private  async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new HomeClient());
    }
}