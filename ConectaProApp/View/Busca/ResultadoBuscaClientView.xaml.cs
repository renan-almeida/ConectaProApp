using ConectaProApp.Models;
using ConectaProApp.ViewModels.Cliente;
using ConectaProApp.ViewModels.Prestador;
using System.Text.Json;

namespace ConectaProApp.View.Busca;

//[QueryProperty(nameof(PrestadoresJson), "Prestadores")]
public partial class ResultadoBuscaClientView : ContentPage
{
    /*   private string _prestadoresJson;

        public string PrestadoresJson
        {
            get => _prestadoresJson;
            set
            {
                _prestadoresJson = Uri.UnescapeDataString(value);
                var prestadores = JsonSerializer.Deserialize<List<Models.Prestador>>(_prestadoresJson);
                BindingContext = new ResultadoBuscaClientViewModel(prestadores);
            }
        }*/
public ResultadoBuscaClientView()
	{
		InitializeComponent();
	}
}