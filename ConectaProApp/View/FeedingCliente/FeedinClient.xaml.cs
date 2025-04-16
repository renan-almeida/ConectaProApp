using ConectaProApp.ViewModels.FeedingCliente;
namespace ConectaProApp.View.FeedingCliente;

public partial class FeedinClient : ContentPage
{
	public FeedinClient()
	{
		InitializeComponent();
        BindingContext = new FeedingClient();
    }
}