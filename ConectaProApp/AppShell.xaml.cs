using ConectaProApp.View.Usuario;

namespace ConectaProApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login", typeof(LoginView));
        }

        
    

    }
}
