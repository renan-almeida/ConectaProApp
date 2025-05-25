using ConectaProApp.Models;
using ConectaProApp.ViewModels.Cliente;
using ConectaProApp.ViewModels.Prestador;
using System.Text.Json;

namespace ConectaProApp.View.Busca;
public partial class ResultadoBuscaClientView : ContentPage
{
    public ResultadoBuscaClientView()
    {
        InitializeComponent();
    }

    private void OnVoltarClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

}
