using Mopups.Services;
using System;
using Microsoft.Maui.Controls;
using Mopups.Pages;
using ConectaProApp.ViewModels.Usuarios;
using CommunityToolkit.Maui.Views;

namespace ConectaProApp.PopUp
{
    public partial class TipoContaAlert : Popup
    {

        public TipoContaAlert(Action closePopupCallback)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(closePopupCallback);
        }

        private void CancelPopup(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
