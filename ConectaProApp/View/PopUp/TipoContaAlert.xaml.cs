using Mopups.Services;
using Mopups.Pages;
using ConectaProApp.View.CadastroCliente;
using ConectaProApp.View.CadastroPrestador;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace ConectaProApp.PopUp
{
    public partial class TipoContaAlert : PopupPage
    {
        public Command SelectedClientCommand { get; set; }
        public Command SelectedProviderCommand { get; set; }

        public TipoContaAlert()
        {
            InitializeComponent();
            InitializeCommands();
            BindingContext = this;
        }

        public void InitializeCommands()
        {
            SelectedClientCommand = new Command(async () => await SelectedClient());
            SelectedProviderCommand = new Command(async () => await SelectedProvider());
        }

        private async Task SelectedClient()
        {
            await MopupService.Instance.PopAsync(); // Fecha o popup
            await Application.Current.MainPage.Navigation.PushAsync(new EtapaUm()); // Vai para a tela de cadastro de cliente
        }

        private async Task SelectedProvider()
        {
            await MopupService.Instance.PopAsync(); // Fecha o popup
            await Application.Current.MainPage.Navigation.PushAsync(new EtapaUm_Prestador()); // Vai para a tela de cadastro de prestador
        }

        private async void CancelPopup(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync(); // Fecha o popup sem fazer nada
        }
    }
}
