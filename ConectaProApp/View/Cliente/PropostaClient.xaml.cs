using CommunityToolkit.Maui.Views;
using ConectaProApp.View.PopUp;
using ConectaProApp.ViewModels.Cliente;

namespace ConectaProApp.View.Cliente;

public partial class PropostaClient : ContentPage
{
    private PropostaClientViewModel propostaClientViewModel;
	public PropostaClient(int idPrestador)
	{
		InitializeComponent();
        pickerPrevisaoInicio.MinimumDate = DateTime.Today;
        pickerPrevisaoInicio.MaximumDate = DateTime.Today.AddDays(365);
        pickerPrevisaoInicio.Date = DateTime.Today;
        propostaClientViewModel = new PropostaClientViewModel(idPrestador);
        BindingContext = propostaClientViewModel;
    }

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");

    }

    private async void OnAvatarClientTapped(object sender, EventArgs e)
    {
        // Verifica se o sender é uma imagem
        if (sender is Image image)
        {
            // Realiza o zoom (aumenta o tamanho)
            await image.ScaleTo(1.5, 500, Easing.CubicInOut);

            // Opcional: Retorna ao tamanho original após um pequeno atraso
            await Task.Delay(500);
            await image.ScaleTo(1.0, 250, Easing.CubicInOut);
        }
    }
}