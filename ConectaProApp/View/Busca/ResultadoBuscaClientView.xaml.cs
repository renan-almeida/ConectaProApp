using ConectaProApp.Models;
using ConectaProApp.ViewModels.Cliente;
using ConectaProApp.ViewModels.Prestador;
using System.Text.Json;
using Microsoft.Maui.Controls;

namespace ConectaProApp.View.Busca;

[QueryProperty(nameof(PrestadoresJson), "Prestadores")]
public partial class ResultadoBuscaClientView : ContentPage
{
       private string _prestadoresJson;

        public string PrestadoresJson
        {
            get => _prestadoresJson;
            set
            {
                _prestadoresJson = Uri.UnescapeDataString(value);
                var prestadores = JsonSerializer.Deserialize<List<Models.Prestador>>(_prestadoresJson);
                BindingContext = new ResultadoBuscaClientViewModel(prestadores);
            }
        }

    public void OnVoltarClicked(object sender, TappedEventArgs e)
    {
        // Exemplo: Voltar para a página anterior
        Navigation.PopAsync();
        DisplayAlert("Info", "Foto da empresa clicada.", "OK");
    }

    public void OnFotoEmpresaClicked(object sender, TappedEventArgs e)
    {
        // Lógica simples apenas para corrigir o erro
        DisplayAlert("Info", "Foto da empresa clicada.", "OK");
    }

    public ResultadoBuscaClientView()
	{
		InitializeComponent();
	}
}