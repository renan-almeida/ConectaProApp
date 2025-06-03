using System.Text.Json;
using ConectaProApp.Models;
using ConectaProApp.ViewModels.Prestador;

namespace ConectaProApp.View.Busca;
public partial class ResultadoBuscaPrestadorView : ContentPage
{
    private ResultadoBuscaPrestadorViewModel rsPrestadorViewModel = new ResultadoBuscaPrestadorViewModel(); 
    public ResultadoBuscaPrestadorView()
    {
        InitializeComponent();
        BindingContext = rsPrestadorViewModel;
    }

    private void OnVoltarClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }
}