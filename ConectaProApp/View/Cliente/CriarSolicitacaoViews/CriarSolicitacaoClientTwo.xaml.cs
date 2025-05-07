using ConectaProApp.ViewModels.Cliente;


namespace ConectaProApp.View.Cliente.CriarSolicitacaoViews;

public partial class CriarSolicitacaoClientTwo : ContentPage
{
	private CriarSolicitacaoViewModel _criarSolicitacaoViewModel;
	public CriarSolicitacaoClientTwo()
	{
		InitializeComponent();
		_criarSolicitacaoViewModel = new CriarSolicitacaoViewModel();
		BindingContext = _criarSolicitacaoViewModel;
	}

    private void OnFotoEmpresaClicked(object sender, EventArgs e)
    {
        Shell.Current.FlyoutIsPresented = true;
    }

    private async void OnVoltarClicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage.Navigation.PushAsync(new CriarSolicitacaoClient());
    }

    private bool isUpdatingValor = false;

    private void OnValorPropostoChanged(object sender, TextChangedEventArgs e)
    {
        if (isUpdatingValor)
            return;

        isUpdatingValor = true;

        var entry = sender as Entry;
        string texto = new string(entry.Text.Where(char.IsDigit).ToArray());

        if (string.IsNullOrEmpty(texto))
        {
            entry.Text = "";
            isUpdatingValor = false;
            return;

            // Convertendo para decimal e formatando como moeda
            if(decimal.TryParse(texto, out decimal valorDecimal))
            {
                valorDecimal /= 100; // aqui formatamos para ter ponto apos duas casas(ex: 1234 = 12.34)
                entry.Text = string.Format(System.Globalization.CultureInfo.GetCultureInfo("pt-br"), "R$ {0:N2", valorDecimal);

            }

            // Movendo o cursor para o final
            entry.CursorPosition = entry.Text.Length;
            isUpdatingValor = false;

            // Enviando o valor para a ViewModel
            if (BindingContext is CriarSolicitacaoViewModel vm)
            {
                vm.ValorProposto = valorDecimal;
            }

        }

    }
}