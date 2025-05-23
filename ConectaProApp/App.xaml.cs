﻿using ConectaProApp.Services;

namespace ConectaProApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            bool modoDev = true;

            if (modoDev)
            {
                MainPage = new AppShell();

                Dispatcher.Dispatch(async () =>
                {
                    await Shell.Current.GoToAsync("//cliente");
                });
            }
            else
            {
                bool usuarioLogado = Preferences.Get("UsuarioLogado", false);
                string tipoUsuario = Preferences.Get("TipoUsuario", "");

                if (usuarioLogado)
                {
                    MainPage = new AppShell();

                    // Aguarda a UI carregar antes de redirecionar
                     Dispatcher.Dispatch(async () =>
                    {
                        // Levando o Usuario correto para a home baseada no seu tipo de usuario
                        if (tipoUsuario == "PRESTADOR")
                        {
                            await Shell.Current.GoToAsync("//prestador");
                        }
                        else if (tipoUsuario == "CLIENTE")
                        {
                            await Shell.Current.GoToAsync("//cliente");
                        }
                        else
                        {
                            await (MainPage as NavigationPage).PushAsync(new MainPage());
                        }
                    });
                }
                else
                {
                    MainPage = new NavigationPage(new MainPage());
                }
            }
                


            }
       

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(MainPage);
        }

       

    }
}