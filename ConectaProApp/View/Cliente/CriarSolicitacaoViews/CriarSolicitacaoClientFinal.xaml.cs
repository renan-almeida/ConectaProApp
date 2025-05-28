using CommunityToolkit.Maui.Views;
using ConectaProApp.View.PopUp;
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

    private async void OnFinalizarClicked(object sender, EventArgs e)
    {
        if (BindingContext is CriarSolicitacaoViewModel vm)
        {
            bool sucesso = await vm.FinalizarSolicitacao();

            if (sucesso)
            {
                var popup = new CriarSolicitacaoSucessoPopup();
                popup.Closed += async (s, args) =>
                {
                    await Navigation.PushAsync(new HomeClient());
                };

                await this.ShowPopupAsync(popup);
            }
            else
            {
                await DisplayAlert("Erro", "N�o foi poss�vel criar a solicita��o.", "OK");
            }
        }
    }



    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");

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
                var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
                var imageBytes = memoryStream.ToArray();

                ImageServico.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));

                if (BindingContext is CriarSolicitacaoViewModel vm)
                {
                    vm.FotoServico = Convert.ToBase64String(imageBytes);
                }

            }
        }
        catch (Exception ex)
        {

            await DisplayAlert("Erro", $"Erro ao carregar a imagem: {ex.Message}", "OK");
        }
    }
}