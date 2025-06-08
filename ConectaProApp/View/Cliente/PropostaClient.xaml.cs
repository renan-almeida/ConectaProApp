using CommunityToolkit.Maui.Views;
using ConectaProApp.View.PopUp;
using ConectaProApp.ViewModels.Cliente;

namespace ConectaProApp.View.Cliente;

public partial class PropostaClient : ContentPage
{
    private PropostaClientViewModel propostaClientViewModel;
	public PropostaClient(int idPrestador)
	{
		InitializeComponent();
        pickerPrevisaoInicio.MinimumDate = DateTime.Today;
        pickerPrevisaoInicio.MaximumDate = DateTime.Today.AddDays(365);
        pickerPrevisaoInicio.Date = DateTime.Today;
        propostaClientViewModel = new PropostaClientViewModel(idPrestador);
        BindingContext = propostaClientViewModel;
    }

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");

    }

    private bool _isUpdatingValorDisposto = false;
    private void OnValorDispostoTextChanged(object sender, TextChangedEventArgs e)
    {
        if (_isUpdatingValorDisposto)
            return;

        _isUpdatingValorDisposto = true;

        var entry = sender as Entry;
        if (entry == null) return;

        if (e.NewTextValue == null) return;

        // Remove tudo que não é número
        string digitsOnly = new string(e.NewTextValue.Where(char.IsDigit).ToArray());

        if (string.IsNullOrWhiteSpace(digitsOnly))
        {
            entry.Text = string.Empty;
            _isUpdatingValorDisposto = false;
            return;
        }

        // Converte para decimal (assumindo 2 casas decimais)
        if (decimal.TryParse(digitsOnly, out decimal valorDecimal))
        {
            valorDecimal /= 100; // Para colocar as casas decimais
            entry.Text = valorDecimal.ToString("C", new System.Globalization.CultureInfo("pt-BR"));
        }

        // Move o cursor pro final
        entry.CursorPosition = entry.Text.Length;

        _isUpdatingValorDisposto = false;
    }
}