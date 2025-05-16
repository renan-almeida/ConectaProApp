using System.Text.Json;
using ConectaProApp.Models;
using ConectaProApp.ViewModels.Prestador;

namespace ConectaProApp.View.Busca;

[QueryProperty(nameof(ServicosJson), "Servicos")]
public partial class ResultadoBuscaPrestadorView : ContentPage
{
    private string _servicosJson;

    public string ServicosJson
    {
        get => _servicosJson;
        set
        {
            _servicosJson = Uri.UnescapeDataString(value);
            var servicos = JsonSerializer.Deserialize<List<Servico>>(_servicosJson);
            BindingContext = new ResultadoBuscaPrestadorViewModel(servicos);
        }
    }

    public ResultadoBuscaPrestadorView()
    {
        InitializeComponent();
    }
}