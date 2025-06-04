using ConectaProApp.Services;
using ConectaProApp.ViewModels.Solicitacaos;
using ConectaProApp.Services.Azure;
using ConectaPro.Controllers; // Para Permissions


namespace ConectaProApp.View.Cliente;

public partial class MinhaContaClient : ContentPage
{

    private readonly ApiService _apiService;
    private readonly BlobService _blobService;

    public MinhaContaClient(int IdEmpresa)
    {
        InitializeComponent();

        _apiService = new ApiService();
        _blobService = new BlobService(_apiService);


        int idCliente = Preferences.Get("id", 0); // ou de onde você obtém o id do cliente logado
        var vm = new SolicitacaoViewModel(SolicitacaoViewModel.TipoUsuario.Cliente, idCliente);
        this.BindingContext = vm;

        //BindingContext = new PerfilEmpresaViewModel(idEmpresa);

        // O binding do XAML já cuida da exibição da imagem do avatar pelo ViewModel.

        NomeEntry.Text = Preferences.Get("NomeCliente", "Etec Horácio Augusto");
        DescricaoEditor.Text = Preferences.Get("DescricaoCliente", "Somos da Etec Horácio Augusto da Silveira.");
    }

    // 

    private async void OnHeaderClienteTapped(object sender, EventArgs e)
    {
        try
        {
            if (!await VerificarPermissoesAsync())
            {
                await DisplayAlert("Permissão negada", "O app precisa de permissão para acessar suas fotos.", "OK");
                return;
            }

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

   private async Task<bool> VerificarPermissoesAsync()
{
#if ANDROID
    if (DeviceInfo.Version.Major >= 13)
    {
        var status = await Permissions.CheckStatusAsync<Permissions.Media>();
        if (status != PermissionStatus.Granted)
            status = await Permissions.RequestAsync<Permissions.Media>();
        return status == PermissionStatus.Granted;
    }
    else
    {
        var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
        if (status != PermissionStatus.Granted)
            status = await Permissions.RequestAsync<Permissions.StorageRead>();
        return status == PermissionStatus.Granted;
    }
#else
    var status = await Permissions.CheckStatusAsync<Permissions.Photos>();
    if (status != PermissionStatus.Granted)
        status = await Permissions.RequestAsync<Permissions.Photos>();
    return status == PermissionStatus.Granted;
#endif
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