using Mopups.Services;
using System;
using Microsoft.Maui.Controls;
using Mopups.Pages;
using ConectaProApp.ViewModels.Usuarios;

namespace ConectaProApp.PopUp
{
    public partial class TipoContaAlert : PopupPage
    {
        private LoginViewModel _loginViewModel;
        public TipoContaAlert()
        {
            InitializeComponent();

            _loginViewModel = new LoginViewModel();
            BindingContext = _loginViewModel;
            
        }

        private async void CancelPopup(object sender, EventArgs e)
        {
            await MopupService.Instance.PopAsync(); // Fecha o popup sem fazer nada
        }
    }
}
