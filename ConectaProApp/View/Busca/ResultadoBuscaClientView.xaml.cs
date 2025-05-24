using ConectaProApp.Models;
using ConectaProApp.ViewModels.Cliente;
using ConectaProApp.ViewModels.Prestador;
using System.Text.Json;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

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
        Navigation.PopAsync();
    }

    public void OnFotoEmpresaClicked(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true; 
    }


    public ResultadoBuscaClientView()
	{
		InitializeComponent();
	}
}