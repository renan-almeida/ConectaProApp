namespace ConectaProApp.View.Cliente;

public partial class MinhaContaClient : ContentPage
{
	public MinhaContaClient()
	{
		InitializeComponent();

        var avatarPath = Path.Combine(FileSystem.AppDataDirectory, "avatar_client.png");
        var headerPath = Path.Combine(FileSystem.AppDataDirectory, "header_cliente.png");

        // Verificar se as imagens existem e carregá-las
        if (File.Exists(avatarPath))
        {
            avatarImageClient.Source = ImageSource.FromFile(avatarPath);
        }

        if (File.Exists(headerPath))
        {
            headerClienteImage.Source = ImageSource.FromFile(headerPath);
        }

        NomeEntry.Text = Preferences.Get("NomeCliente", "Etec Horácio Augusto");
        DescricaoEditor.Text = Preferences.Get("DescricaoCliente", "Somos da Etec Horácio Augusto da Silveira.");
    }

    private async void OnAvatarClientTapped(object sender, EventArgs e)
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
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "avatar_client.png");

                // Salvar a imagem no sistema de arquivos
                using (var fileStream = File.Create(filePath))
                {
                    await stream.CopyToAsync(fileStream);
                }

                // Atualizar a imagem do avatar
                avatarImageClient.Source = ImageSource.FromFile(filePath);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Erro", "Não foi possível selecionar a foto: " + ex.Message, "OK");
        }
    }

    private async void OnHeaderClienteTapped(object sender, EventArgs e)
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
                var filePath = Path.Combine(FileSystem.AppDataDirectory, "header_cliente.png");

                // Salvar a imagem no sistema de arquivos
                using (var fileStream = File.Create(filePath))
                {
                    await stream.CopyToAsync(fileStream);
                }

                // Atualizar a imagem de "tecnologia"
                headerClienteImage.Source = ImageSource.FromFile(filePath);
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

            // Salvar o nome localmente
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
            // Obter o novo valor da descrição
            string novaDescricao = e.NewTextValue;

            // Salvar a descrição localmente
            Preferences.Set("DescricaoCliente", novaDescricao);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao salvar a descrição: {ex.Message}");
        }
    }
}
