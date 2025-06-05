using ConectaProApp.Services;
using ConectaProApp.ViewModels.Solicitacaos;
using System;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{
    public class MinhaContaClienteViewModel
    {
        public PerfilEmpresaViewModel PerfilVM { get; set; }
        public SolicitacaoViewModel SolicitacaoVM { get; set; }

        public Command<string> SelecionarAbaCommand { get; }

        public MinhaContaClienteViewModel(int idEmpresa, int idCliente, ApiService apiService)
        {
            PerfilVM = new PerfilEmpresaViewModel(idEmpresa, apiService);
            SolicitacaoVM = new SolicitacaoViewModel(SolicitacaoViewModel.TipoUsuario.Cliente, idCliente);

            SelecionarAbaCommand = new Command<string>(async (param) =>
            {
                // Oculta todas as abas primeiro
                SolicitacaoVM.SolicitacoesClienteVisivel = false;
                SolicitacaoVM.PropostasClienteVisivel = false;
                SolicitacaoVM.ServicosPrestadorVisivel = false;
                PerfilVM.HistoricoClienteVisivel = false;

                switch (param)
                {
                    case "SolicitacoesCliente":
                        SolicitacaoVM.SolicitacoesClienteVisivel = true;
                        break;
                    case "PropostasCliente":
                        SolicitacaoVM.PropostasClienteVisivel = true;
                        break;
                    case "HistoricoCliente":
                        await PerfilVM.ExibirHistorico();
                        break;
                }
            });
        }
    }
}
