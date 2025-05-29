using ConectaProApp.Services.Azure;

namespace ConectaProApp.View.Cliente;

public partial class MinhaContaClient : ContentPage
{
    private readonly BlobService _blobService = new BlobService();

    public MinhaContaClient()
    {
        InitializeComponent();

        var avatarUrl = Preferences.Get("CaminhoAvatarCliente", null);
        var headerUrl = Preferences.Get("CaminhoHeaderCliente", null);

        if (!string.IsNullOrEmpty(avatarUrl))
            avatarImageClient.Source = ImageSource.FromUri(new Uri(avatarUrl));

        if (!string.IsNullOrEmpty(headerUrl))
            headerClienteImage.Source = ImageSource.FromUri(new Uri(headerUrl));

        NomeEntry.Text = Preferences.Get("NomeCliente", "Etec Horácio Augusto");
        DescricaoEditor.Text = Preferences.Get("DescricaoCliente", "Somos da Etec Horácio Augusto da Silveira.");
    }

    private async void OnAvatarClientTapped(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Selecione uma foto" });

            if (photo != null)
            {
                var url = await _blobService.UploadImagemAsync(photo);
                if (url != null)
                {
                    Preferences.Set("CaminhoAvatarCliente", url);
                    avatarImageClient.Source = ImageSource.FromUri(new Uri(url));
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Erro ao selecionar a foto: " + ex.Message, "OK");
        }
    }

    private async void OnHeaderClienteTapped(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Selecione uma imagem para tecnologia" });

            if (photo != null)
            {
                var url = await _blobService.UploadImagemAsync(photo);
                if (url != null)
                {
                    Preferences.Set("CaminhoHeaderCliente", url);
                    headerClienteImage.Source = ImageSource.FromUri(new Uri(url));
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
            Preferences.Set("NomeCliente", novoNome);
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
            Preferences.Set("DescricaoCliente", novaDescricao);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar a descrição: {ex.Message}");
        }
    }

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}
