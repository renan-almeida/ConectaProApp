using ConectaProApp.ViewModels.Solicitacaos;
using ConectaProApp.Services.Azure;

namespace ConectaProApp.View.Prestador;

public partial class MinhaContaPrestador : ContentPage
{
    private readonly BlobService _blobService = new BlobService();

    public MinhaContaPrestador()
    {
        InitializeComponent();

        int idPrestador = Preferences.Get("id", 0); // ou de onde você obtém o id do prestador logado
        var vm = new SolicitacaoViewModel(SolicitacaoViewModel.TipoUsuario.Prestador, idPrestador);
        this.BindingContext = vm;

        var avatarUrl = Preferences.Get("CaminhoAvatarPrestador", null);
        var headerUrl = Preferences.Get("CaminhoHeaderPrestador", null);

        if (!string.IsNullOrEmpty(avatarUrl))
            avatarPrestadorImage.Source = ImageSource.FromUri(new Uri(avatarUrl));

        if (!string.IsNullOrEmpty(headerUrl))
            HeaderPrestadorImage.Source = ImageSource.FromUri(new Uri(headerUrl));

        NomeEntry.Text = Preferences.Get("NomePrestador", "Claudio de Freitas Silva");
        DescricaoEditor.Text = Preferences.Get("DescricaoPrestador", "Olá, me chamo Claudio, tenho 33 anos e sou programador Back-End Sênior.");
    }

    public async void OnAvatarPrestadorTapped(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Selecione uma foto" });

            if (photo != null)
            {
                var url = await _blobService.UploadImagemAsync(photo);
                if (url != null)
                {
                    Preferences.Set("CaminhoAvatarPrestador", url);
                    avatarPrestadorImage.Source = ImageSource.FromUri(new Uri(url));
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Erro ao selecionar a foto: " + ex.Message, "OK");
        }
    }

    public async void OnHeaderPrestadorTapped(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Selecione uma imagem para tecnologia" });

            if (photo != null)
            {
                var url = await _blobService.UploadImagemAsync(photo);
                if (url != null)
                {
                    Preferences.Set("CaminhoHeaderPrestador", url);
                    HeaderPrestadorImage.Source = ImageSource.FromUri(new Uri(url));
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Erro ao selecionar a imagem: " + ex.Message, "OK");
        }
    }

    private void OnNomeChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string novoNome = e.NewTextValue;
            Preferences.Set("NomePrestador", novoNome);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar o nome: {ex.Message}");
        }
    }

    private void OnDescricaoChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            string novaDescricao = e.NewTextValue;
            Preferences.Set("DescricaoPrestador", novaDescricao);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar a descrição: {ex.Message}");
        }
    }

    private void OnFotoPrestadorClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}