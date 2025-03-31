using Mopups.Services;
using System;
using Microsoft.Maui.Controls;
using Mopups.Pages;

namespace ConectaProApp.PopUp
{
    public partial class TipoContaAlert :  PopupPage
    {
        public TipoContaAlert()
        {
            InitializeComponent();
            InitializeCommands();
        }
        public Command SelectedClientCommand;
        public Command SelectedProviderCommand;

        public void InitializeCommands()
        {
            SelectedClientCommand = new Command(async () => await SelectedClient());
            SelectedProviderCommand = new Command(async () => await SelectedProvider());
        }


        private async Task SelectedClient()
        {
            await MopupService.Instance.PopAsync(); // Fecha o popup
            await Shell.Current.GoToAsync("CadastroCliente"); // Vai para a tela de cadastro de cliente
        }

        private async Task SelectedProvider()
        {
            await MopupService.Instance.PopAsync(); // Fecha o popup
            await Shell.Current.GoToAsync("CadastroPrestador"); // Vai para a tela de cadastro de prestador
        }

        private async void CancelPopup(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync(); // Fecha o popup sem fazer nada
        }
    }
}
