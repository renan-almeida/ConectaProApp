using ConectaProApp.Services;
using ConectaProApp.Services.Azure;
using ConectaProApp.ViewModels.Foto;
using ConectaProApp.ViewModels.Solicitacaos;
using System;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Cliente
{
    public class MinhaContaClienteViewModel
    {
        public PerfilEmpresaViewModel PerfilVM { get; set; }
        public SolicitacaoViewModel SolicitacaoVM { get; set; }

        public FotoViewModel FotoVMHeader { get; }
        public FotoViewModel FotoVMAvatar { get; }

        public Command<string> SelecionarAbaCommand { get; }



        public MinhaContaClienteViewModel(int idEmpresa, int idCliente, ApiService apiService)
        {
            PerfilVM = new PerfilEmpresaViewModel(idEmpresa, apiService);
            SolicitacaoVM = new SolicitacaoViewModel(SolicitacaoViewModel.TipoUsuario.Cliente, idCliente);

            var blobService = new BlobService(apiService);

            FotoVMHeader = new FotoViewModel(blobService, apiService, "https://conectapro-api.azurewebsites.net/fotos/upload", "CaminhoHeaderCliente");
            FotoVMAvatar = new FotoViewModel(blobService, apiService, "https://conectapro-api.azurewebsites.net/fotos/upload", "CaminhoAvatarCliente");

            // Associa o avatar ao SolicitacaoVM, caso ainda esteja usando dentro dele
            SolicitacaoVM = new SolicitacaoViewModel(SolicitacaoViewModel.TipoUsuario.Cliente, idCliente);

           
            FotoVMHeader = new FotoViewModel(blobService, apiService, "https://conectapro-api.azurewebsites.net/fotos/upload", "CaminhoHeaderCliente");

            SelecionarAbaCommand = new Command<string>(async (param) =>
            {
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
