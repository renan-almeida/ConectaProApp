using ConectaProApp.Models;
using ConectaProApp.ViewModels.Cliente;
using ConectaProApp.ViewModels.Prestador;
using System.Text.Json;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace ConectaProApp.View.Busca;

public partial class ResultadoBuscaClientView : ContentPage
{

    private ResultadoBuscaClientViewModel rsBuscaClientViewModel = new ResultadoBuscaClientViewModel();
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
        BindingContext = rsBuscaClientViewModel;
	}
}