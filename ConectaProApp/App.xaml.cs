using Microsoft.Maui.Controls;
using Mopups.Services;

namespace ConectaProApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

         
            MainPage = new NavigationPage(new View.Usuario.LoginView());
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}