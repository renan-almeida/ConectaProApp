using ConectaProApp.ViewModels.Cliente;


namespace ConectaProApp.View.Cliente.CriarSolicitacaoViews;

public partial class CriarSolicitacaoClientFinal : ContentPage
{
	private CriarSolicitacaoViewModel _criarSolicitacaoViewModel;
	public CriarSolicitacaoClientFinal()
	{
		InitializeComponent();
		_criarSolicitacaoViewModel = new CriarSolicitacaoViewModel();
		this.BindingContext = _criarSolicitacaoViewModel;
	}

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new CriarSolicitacaoClientTwo());
    }

    private async void OnClickedImageServiceTapped(object sender, EventArgs e)
    {
        var escolha = await
            DisplayActionSheet("Selecione a origem da imagem", "Cancelar", null, "Galeria", "Camera");

        FileResult foto = null;

        try
        {
            if (escolha == "Galeria")
            {
                foto = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Escolha uma imagem para seu servico"
                });
            }
            else if (escolha == "Camera")
            {
                foto = await MediaPicker.CapturePhotoAsync();
            }

            if (foto != null)
            {
                using var stream = await foto.OpenReadAsync();
            }
        }
        catch (Exception)
        {

            throw;
        }
    }
}