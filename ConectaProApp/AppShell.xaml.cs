using ConectaProApp.View.Cliente;
using ConectaProApp.View.Usuario;
using ConectaProApp.View.FeedingCliente;
namespace ConectaProApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("login", typeof(LoginView));
            Routing.RegisterRoute("registerclienttwo", typeof(RegisterClientTwo));
            Routing.RegisterRoute(nameof(FeedinClient), typeof(FeedinClient));
        }

        
    

    }
}
