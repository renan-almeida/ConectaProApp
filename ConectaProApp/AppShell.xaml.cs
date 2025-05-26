using ConectaProApp.View.Cliente;
using ConectaProApp.View.Usuario;
using ConectaProApp.View.Prestador;
using ConectaProApp.View.Busca;

namespace ConectaProApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("prestador", typeof(HomePrestador));
            Routing.RegisterRoute("cliente", typeof(HomeClient));
            Routing.RegisterRoute(nameof(ResultadoBuscaPrestadorView), typeof(ResultadoBuscaPrestadorView));
            Routing.RegisterRoute(nameof(ResultadoBuscaClientView), typeof(ResultadoBuscaClientView));

        }




    }
}
