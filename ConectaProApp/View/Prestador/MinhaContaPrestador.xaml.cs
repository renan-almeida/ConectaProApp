namespace ConectaProApp.View.Prestador;

public partial class MinhaContaPrestador : ContentPage
{
	public MinhaContaPrestador()
	{
		InitializeComponent();

        var avatarPath = Path.Combine(FileSystem.AppDataDirectory, "avatar_prestador.png");
        var headerPath = Path.Combine(FileSystem.AppDataDirectory, "header_prestador.png");

        if (File.Exists(avatarPath))
        {
            avatarPrestadorImage.Source = ImageSource.FromFile(avatarPath);
        }

        if (File.Exists(headerPath))
        {
            HeaderPrestadorImage.Source = ImageSource.FromFile(headerPath);
        }

        NomeEntry.Text = Preferences.Get("NomePrestador", "Claudio de Freitas Silva");
        DescricaoEditor.Text = Preferences.Get("DescricaoPrestador", "Olá, me chamo Claudio, tenho 33 anos e sou programador Back-End Sênior.");
    }

    private async void OnAvatarTapped(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Selecione uma foto"
            });

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();

                // Caminho para salvar a imagem localmente
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "avatar_prestador.png");

                // Salvar a imagem no sistema de arquivos
                using (var fileStream = File.Create(filePath))
                {
                    await stream.CopyToAsync(fileStream);
                }

                // Atualizar a imagem do avatar
                avatarPrestadorImage.Source = ImageSource.FromFile(filePath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Não foi possível selecionar a foto: " + ex.Message, "OK");
        }
    }

    private async void OnHeaderPrestadorTapped(object sender, EventArgs e)
    {
        try
        {
            var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Selecione uma imagem para tecnologia"
            });

            if (photo != null)
            {
                var stream = await photo.OpenReadAsync();

                // Caminho para salvar a imagem localmente
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "header_prestador.png");

                // Salvar a imagem no sistema de arquivos
                using (var fileStream = File.Create(filePath))
                {
                    await stream.CopyToAsync(fileStream);
                }

                // Atualizar a imagem de "tecnologia"
                HeaderPrestadorImage.Source = ImageSource.FromFile(filePath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Não foi possível selecionar a imagem: " + ex.Message, "OK");
        }
    }

    private void OnNomeChanged(object sender, TextChangedEventArgs e)
    {
        try
        {
            // Obter o novo valor do nome
            string novoNome = e.NewTextValue;

            // Salvar o nome localmente (exemplo: Preferences ou arquivo)
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
            // Obter o novo valor da descrição
            string novaDescricao = e.NewTextValue;

            // Salvar a descrição localmente (exemplo: Preferences ou arquivo)
            Preferences.Set("DescricaoPrestador", novaDescricao);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar a descrição: {ex.Message}");
        }
    }
}