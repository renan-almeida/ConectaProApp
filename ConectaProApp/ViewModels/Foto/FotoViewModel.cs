using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConectaProApp.Services.Azure;
using ConectaProApp.Services;
using ConectaProApp.Models;
using System.Diagnostics;

namespace ConectaProApp.ViewModels.Foto
{
    public partial class FotoViewModel : ObservableObject
    {
        private readonly BlobService _blobService;
        private readonly ApiService _apiService;
        private readonly string _endpointApi;
        private readonly string _chavePreferencia;

        public FotoViewModel(BlobService blobService, ApiService apiService, string endpointApi, string chavePreferencia)
        {
            _blobService = blobService;
            _apiService = apiService;
            _endpointApi = endpointApi;
            _chavePreferencia = chavePreferencia;

            FotoUrl = Preferences.Get(_chavePreferencia, string.Empty);
            if (!string.IsNullOrEmpty(FotoUrl))
            {
                FonteImagem = ImageSource.FromUri(new Uri(FotoUrl));
            }

            FotografarCommand = new AsyncRelayCommand(FotografarAsync);
            AbrirGaleriaCommand = new AsyncRelayCommand(AbrirGaleriaAsync);
            SelecionarFotoCommand = new AsyncRelayCommand(SelecionarFotoAsync);
        }

        [ObservableProperty] private string fotoUrl;
        [ObservableProperty] private ImageSource fonteImagem;
        [ObservableProperty] private bool isUploading;

        public IRelayCommand FotografarCommand { get; }
        public IRelayCommand AbrirGaleriaCommand { get; }
        public IRelayCommand SelecionarFotoCommand { get; }

        private async Task SelecionarFotoAsync()
        {
            var escolha = await App.Current.MainPage.DisplayActionSheet(
                "Selecionar foto", "Cancelar", null, "Fotografar", "Escolher da Galeria");

            if (escolha == "Fotografar") await FotografarAsync();
            else if (escolha == "Escolher da Galeria") await AbrirGaleriaAsync();
        }

        private async Task FotografarAsync()
        {
            try
            {
                var foto = await MediaPicker.CapturePhotoAsync();
                if (foto != null)
                    await ProcessarImagemAsync(foto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao capturar foto: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao capturar foto: {ex.Message}", "OK");
            }
        }

        private async Task AbrirGaleriaAsync()
        {
            try
            {
                var foto = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Escolha uma imagem"
                });

                if (foto != null)
                    await ProcessarImagemAsync(foto);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao abrir galeria: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao abrir galeria: {ex.Message}", "OK");
            }
        }

        private async Task ProcessarImagemAsync(FileResult arquivo)
        {
            try
            {
                IsUploading = true;

                using var stream = await arquivo.OpenReadAsync();
                var url = await _blobService.UploadImagemAsync(arquivo);

                if (!string.IsNullOrEmpty(url))
                {
                    FotoUrl = url;
                    FonteImagem = ImageSource.FromUri(new Uri(url));
                    Preferences.Set(_chavePreferencia, url);

                    bool sucesso = await _apiService.AtualizarFotoPerfilAsync(_endpointApi, url);

                    if (sucesso)
                    {
                        await App.Current.MainPage.DisplayAlert("Sucesso", "Imagem enviada com sucesso!", "OK");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Erro", "Falha ao atualizar a foto no servidor.", "OK");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Erro", "Erro ao obter URL da imagem", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro no processamento da imagem: {ex.Message}");
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro interno: {ex.Message}", "OK");
            }
            finally
            {
                IsUploading = false;
            }
        }
    }
}
