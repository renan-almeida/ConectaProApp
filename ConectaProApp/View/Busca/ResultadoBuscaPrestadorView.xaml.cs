using ConectaProApp.Models;
using ConectaProApp.ViewModels.Prestador;

namespace ConectaProApp.View.Busca;

public partial class ResultadoBuscaPrestadorView : ContentPage
{
	public ResultadoBuscaPrestadorView(List<Servico> servicos)
	{
		InitializeComponent();
		BindingContext = new ResultadoBuscaPrestadorViewModel(servicos);

	}
}