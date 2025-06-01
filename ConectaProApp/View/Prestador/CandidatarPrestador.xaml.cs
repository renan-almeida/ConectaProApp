using CommunityToolkit.Maui.Views;
using ConectaProApp.View.Cliente;
using ConectaProApp.View.PopUp;
using ConectaProApp.ViewModels.Prestador;

namespace ConectaProApp.View.Prestador;

public partial class CandidatarPrestador : ContentPage
{
    private CandidaturaPrestadorViewModel candidaturaPrestadorViewModel;
	public CandidatarPrestador(int idSolicitacao)
	{
		InitializeComponent();
        pickerPrevisaoInicio.MinimumDate = DateTime.Today;
        pickerPrevisaoInicio.MaximumDate = DateTime.Today.AddDays(365);
        pickerPrevisaoInicio.Date = DateTime.Today;
        pickerPrevisaoFim.MinimumDate = DateTime.Today;
        pickerPrevisaoFim.MaximumDate = DateTime.Today.AddDays(365);
        pickerPrevisaoFim.Date = DateTime.Today;
        candidaturaPrestadorViewModel = new CandidaturaPrestadorViewModel(idSolicitacao);
        BindingContext = candidaturaPrestadorViewModel;
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
    private void OnFotoPrestadorClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");

    }


    private async void EnviarProposta_Clicked(object sender, EventArgs e)
    {
        var popup = new TestePopup();
        await this.ShowPopupAsync(popup);
    }
}