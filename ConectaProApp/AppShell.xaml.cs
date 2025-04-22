using ConectaProApp.View.Cliente;
using ConectaProApp.View.Usuario;
using ConectaProApp.View.Prestador;

namespace ConectaProApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("prestador", typeof(HomePrestador));
            Routing.RegisterRoute("cliente", typeof(HomeClient));
            

        }




    }
}
