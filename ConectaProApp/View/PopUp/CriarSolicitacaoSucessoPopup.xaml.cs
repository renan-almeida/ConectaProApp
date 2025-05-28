namespace ConectaProApp.View.PopUp;
using CommunityToolkit.Maui.Views;

public partial class CriarSolicitacaoSucessoPopup : Popup
{
	public CriarSolicitacaoSucessoPopup()
	{
		InitializeComponent();

        Task.Run(async () =>
        {
            await Task.Delay(3000);
            MainThread.BeginInvokeOnMainThread(() => Close());
        });
    }
}