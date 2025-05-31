using ConectaProApp.ViewModels.Solicitacaos;
using ConectaProApp.Services.Azure;
using ConectaProApp.Services;
using Microsoft.Maui.Controls;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ConectaProApp.View.Prestador
{
    public partial class MinhaContaPrestador : ContentPage
    {
        private readonly ApiService _apiService;
        private readonly BlobService _blobService;

        public MinhaContaPrestador()
        {
            InitializeComponent();

            //  Inicialização dos serviços no construtor
            _apiService = new ApiService();
            _blobService = new BlobService(_apiService);

            int idPrestador = Preferences.Get("id", 0);
            var vm = new SolicitacaoViewModel(SolicitacaoViewModel.TipoUsuario.Prestador, idPrestador);
            BindingContext = vm;

            ConfigurarImagens();
            ConfigurarDadosUsuario();
        }

        private void ConfigurarImagens()
        {
            var avatarUrl = Preferences.Get("CaminhoAvatarPrestador", null);
            var headerUrl = Preferences.Get("CaminhoHeaderPrestador", null);

            if (!string.IsNullOrEmpty(avatarUrl))
                avatarPrestadorImage.Source = ImageSource.FromUri(new Uri(avatarUrl));

            if (!string.IsNullOrEmpty(headerUrl))
                HeaderPrestadorImage.Source = ImageSource.FromUri(new Uri(headerUrl));
        }

        private void ConfigurarDadosUsuario()
        {
            NomeEntry.Text = Preferences.Get("NomePrestador", "Claudio de Freitas Silva");
            DescricaoEditor.Text = Preferences.Get("DescricaoPrestador", "Olá, me chamo Claudio, tenho 33 anos e sou programador Back-End Sênior.");
        }

        public async void OnAvatarPrestadorTapped(object sender, EventArgs e)
        {
            await SelecionarImagemAsync("CaminhoAvatarPrestador", avatarPrestadorImage);
        }

        public async void OnHeaderPrestadorTapped(object sender, EventArgs e)
        {
            await SelecionarImagemAsync("CaminhoHeaderPrestador", HeaderPrestadorImage);
        }

        private async Task SelecionarImagemAsync(string preferenciaChave, Image imageControl)
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Selecione uma foto" });

                if (photo != null)
                {
                    var url = await _blobService.UploadImagemAsync(photo);
                    if (!string.IsNullOrEmpty(url))
                    {
                        Preferences.Set(preferenciaChave, url);
                        imageControl.Source = ImageSource.FromUri(new Uri(url));
                    }
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", $"Erro ao selecionar a foto: {ex.Message}", "OK");
            }
        }

        private void OnNomeChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Preferences.Set("NomePrestador", e.NewTextValue);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao salvar o nome: {ex.Message}");
            }
        }

        private void OnDescricaoChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Preferences.Set("DescricaoPrestador", e.NewTextValue);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao salvar a descrição: {ex.Message}");
            }
        }

        private void OnFotoPrestadorClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;
        }
    }
}
