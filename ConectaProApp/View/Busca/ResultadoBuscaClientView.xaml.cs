using ConectaProApp.Models;
using ConectaProApp.ViewModels.Cliente;
using ConectaProApp.ViewModels.Prestador;
using System.Text.Json;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

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

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    public void OnVoltarClicked(object sender, TappedEventArgs e)
    {
        Navigation.PopAsync();
    }

    public void OnFotoEmpresaClicked(object sender, TappedEventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true; 
    }
}

