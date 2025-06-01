using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Prestador
{
    public class CandidaturaPrestadorViewModel: BaseViewModel
    {
        private readonly PrestadorService pService;
        private readonly ServicoService sService;
        public ICommand CriarCandidaturaCommand { get; set; }
        public int IdSolicitacao { get; set; }
        public string NomeEmpresa { get; set; }
        public CandidaturaPrestadorViewModel(int idSolicitacao)
        {
            IdSolicitacao = idSolicitacao;
            FormasPagto = [.. Enum.GetNames(typeof(FormaPagtoEnum))];
            CriarCandidaturaCommand = new Command(async () => await FinalizarCandidatura());
          //  _ = CarregarNomeEmpresa();
            CarregarFotoEmpresaAsync();
        }

        private decimal valorProposto;
        public decimal ValorProposto
        {
            get => valorProposto;
            set
            {
                valorProposto = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(ValorPropostoFormatado));
            }
        }
        public string ValorPropostoFormatado => $"{valorProposto:C}";

        public ObservableCollection<string> FormasPagto { get; set; }
        private string formaPagtoSelecionado;
        public string FormaPagtoSelecionado
        {
            get => formaPagtoSelecionado;
            set
            {
                formaPagtoSelecionado = value;
                OnPropertyChanged();
            }
        }

        private DateTime previsaoInicio;
        public DateTime PrevisaoInicio
        {
            get => previsaoInicio;
            set
            {
                previsaoInicio = value;
                OnPropertyChanged();
            }
        }

        private DateTime previsaoFim;
        public DateTime PrevisaoFim
        {
            get => previsaoFim;
            set
            {
                previsaoFim = value;
                OnPropertyChanged();
            }
        }

    /*0    private async Task CarregarNomeEmpresa()
        {
            var empresa = await sService.BuscarSolicitacaoPorIdAsync(IdSolicitacao);
            if (empresa != null)
            {
                NomeEmpresa = empresa.NomeFantasia ?? "Empresa";
                OnPropertyChanged(nameof(NomeEmpresa));
            }
        }
    */

        private ImageSource fotoEmpresa;
        public ImageSource FotoEmpresa
        {
            get => fotoEmpresa;
            set
            {
                fotoEmpresa = value;
                OnPropertyChanged();
            }
        }

        private async Task CarregarFotoEmpresaAsync()
        {
                FotoEmpresa = ImageSource.FromFile("empresasemfoto.png");
        }


        public async Task<bool> FinalizarCandidatura()
        {
            Enum.TryParse(FormaPagtoSelecionado, out FormaPagtoEnum formaPagtoEnum);
            var previsaoInicioFormatada = PrevisaoInicio.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
            var previsaoFimFormatada = PrevisaoFim.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!ValidarCampos(out string mensagemErro))
            {
                await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "Ok");
                return false;
            }
            var idPrestador = Preferences.Get("id", string.Empty);
            var novaproposta = new PropostaCreateDTO
            {
                IdPrestador = int.Parse(idPrestador),
                ValorProposta = this.ValorProposto,
                PrevisaoInicio = previsaoInicioFormatada,
                PrevisaoFim = previsaoFimFormatada,
                FormaPagtoEnum = formaPagtoEnum,
                StatusServicoEnum = StatusServicoEnum.ORCAMENTO
            };

            var propostaRegistrado = await pService.EnviarCandidaturaAsync(novaproposta, this.IdSolicitacao);

            if (propostaRegistrado != null)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Solicitação criada com sucesso!",
                                    "Aguarde o contato de um prestador", "OK");
                return true;
            }
            return false;
        }

        public bool ValidarCampos(out string mensagemErro)
        {
            if ( PrevisaoInicio == default || PrevisaoFim == default ||
                 ValorProposto == 0 ||
                 string.IsNullOrWhiteSpace(FormaPagtoSelecionado))
            {
                mensagemErro = "Por favor, preencha todos os campos nescessários antes de criar a solicitação!";
                return false;
            }

            if (PrevisaoFim <= PrevisaoInicio)
            {
                mensagemErro = "A data de fim deve ser posterior à data de início.";
                return false;
            }


            mensagemErro = string.Empty;
            return true;

        }
    }
}
