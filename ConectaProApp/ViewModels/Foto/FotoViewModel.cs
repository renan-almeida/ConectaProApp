using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ConectaProApp.Services.Azure;
using ConectaProApp.Services;
using Microsoft.Maui.Controls;

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
                FonteImagem = FotoUrl;
            }

            FotografarCommand = new AsyncRelayCommand(FotografarAsync);
            AbrirGaleriaCommand = new AsyncRelayCommand(AbrirGaleriaAsync);
            SelecionarFotoCommand = new AsyncRelayCommand(SelecionarFotoAsync);
        }

        [ObservableProperty] private string fotoUrl;
        [ObservableProperty] private string fonteImagem;
        [ObservableProperty] private bool isUploading;

        public IRelayCommand FotografarCommand { get; }
        public IRelayCommand AbrirGaleriaCommand { get; }
        public IRelayCommand SelecionarFotoCommand { get; }

        private async Task SelecionarFotoAsync()
        {
            System.Diagnostics.Debug.WriteLine(">>> SelecionarFotoAsync chamado!");
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
                Debug.WriteLine("Iniciando ProcessarImagemAsync...");
                IsUploading = true;

                if (arquivo == null)
                {
                    Debug.WriteLine("Arquivo recebido é nulo.");
                    await App.Current.MainPage.DisplayAlert("Erro", "Arquivo inválido.", "OK");
                    return;
                }

                Debug.WriteLine($"Nome do arquivo: {arquivo.FileName}, ContentType: {arquivo.ContentType}");

                using var stream = await arquivo.OpenReadAsync();
                Debug.WriteLine("Stream do arquivo aberto com sucesso.");

                var url = await _blobService.UploadImagemAsync(arquivo);
                Debug.WriteLine($"Resultado do UploadImagemAsync: '{url}'");

                if (!string.IsNullOrEmpty(url))
                {
                    FotoUrl = url;
                    FonteImagem = url;
                    Preferences.Set(_chavePreferencia, url);

                    Debug.WriteLine($"URL da imagem salva: {url}");
                    Debug.WriteLine($"Endpoint para atualização: {_endpointApi}");

                    bool sucesso = await _apiService.AtualizarFotoPerfilAsync(_endpointApi, url);
                    Debug.WriteLine($"Resultado da atualização no servidor: {sucesso}");

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
                    Debug.WriteLine("URL retornada pelo upload está vazia ou nula.");
                    await App.Current.MainPage.DisplayAlert("Erro", "Erro ao obter URL da imagem", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro no processamento da imagem: {ex}");
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro interno: {ex.Message}", "OK");
            }
            finally
            {
                IsUploading = false;
                Debug.WriteLine("ProcessarImagemAsync finalizado.");
            }
        }
    }
}