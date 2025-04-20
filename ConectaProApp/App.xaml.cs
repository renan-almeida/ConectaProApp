namespace ConectaProApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            

            bool usuarioLogado = Preferences.Get("UsuarioLogado", false);
            string tipoUsuario = Preferences.Get("TipoUsuario", "");

            if (usuarioLogado)
            {
                MainPage = new AppShell();

                // Aguarda a UI carregar antes de redirecionar
                Device.BeginInvokeOnMainThread(async () =>
                {
                    // Levando o Usuario correto para a home baseada no seu tipo de usuario
                    if (tipoUsuario == "PRESTADOR")
                    {
                        Shell.Current.GoToAsync("//prestador");
                    }
                    else if (tipoUsuario == "CLIENTE")
                    {
                        Shell.Current.GoToAsync("//cliente");
                    }
                    else
                    {
                        MainPage = new MainPage();
                    }
                });
            }
            else
            {
                MainPage = new MainPage();
            }


            }
       

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(MainPage);
        }

    }
}