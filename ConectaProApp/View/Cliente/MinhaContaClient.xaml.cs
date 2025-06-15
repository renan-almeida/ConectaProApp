using ConectaProApp.Services;
using ConectaProApp.Services.Azure;
using ConectaProApp.ViewModels.Solicitacaos;

namespace ConectaProApp.View.Cliente
{
    public partial class MinhaContaClient : ContentPage
    {
        private readonly SolicitacaoViewModel ViewModel;

        public MinhaContaClient() : this(0) // Pode trocar 0 por um ID v�lido padr�o, se necess�rio
        {
        }

        public MinhaContaClient(int idEmpresa)
        {
            InitializeComponent();

            int idCliente = Preferences.Get("id", 0);

            // Instancia apenas o ViewModel principal, que j� cont�m FotoVMHeader e FotoVMAvatar
            ViewModel = new SolicitacaoViewModel(SolicitacaoViewModel.TipoUsuario.Cliente, idCliente);

            // Define o BindingContext da p�gina inteira para o ViewModel principal
            BindingContext = ViewModel;


            DescricaoEditor.Text = Preferences.Get("DescricaoCliente", "Breve descri��o sobre a sua empresa.");
        }

        private void OnDescricaoChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Preferences.Set("DescricaoCliente", e.NewTextValue);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar a descri��o: {ex.Message}");
            }
        }

        private void OnFotoEmpresaClicked(object sender, EventArgs e)
        {
            Shell.Current.FlyoutIsPresented = true;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await ViewModel.CarregarDadosEmpresaAsync();
        }
    }

}
