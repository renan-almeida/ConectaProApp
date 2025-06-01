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

namespace ConectaProApp.ViewModels.Cliente
{
    public class PropostaClientViewModel: BaseViewModel
    {
        private readonly ServicoService sService;
        private readonly PrestadorService pService;
        public ICommand FinalizarPropostaCommand { get; }
        public int IdPrestador { get; set; }
        public string NomePrestador { get; set; }
        public PropostaClientViewModel(int idPrestador)
        {
            sService = new ServicoService();
            NiveisUrgencia = [.. Enum.GetNames(typeof(NvlUrgenciaEnum))];
            FormasPagto = [.. Enum.GetNames(typeof(FormaPagtoEnum))];
            FinalizarPropostaCommand = new Command(async () => await FinalizarProposta());
            IdPrestador = idPrestador;
            _ = CarregarPrestador();
        }

        private string tituloProposta;
        public string TituloProposta
        {
            get => tituloProposta;
            set
            {

                tituloProposta = value;
                OnPropertyChanged();
            }
        }

        private string descProposta;
        public string DescProposta
        {
            get => descProposta;
            set
            {

                descProposta = value;
                OnPropertyChanged();
            }
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

        public ObservableCollection<string> NiveisUrgencia { get; set; }
        private string urgenciaSelecionada;
        public string UrgenciaSelecionada
        {
            get => urgenciaSelecionada;
            set
            {
                urgenciaSelecionada = value;
                OnPropertyChanged();
            }
        }

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
        private async Task CarregarPrestador()
        {
            var prestador = await pService.BuscarPrestadorPorIdAsync(IdPrestador);
            if (prestador != null)
            {
                NomePrestador = prestador.Nome ?? "prestador";
                OnPropertyChanged(nameof(NomePrestador));
            }
        }

        public async Task<bool> FinalizarProposta()
        {


            Enum.TryParse(UrgenciaSelecionada, out NvlUrgenciaEnum urgenciaEnum);
            Enum.TryParse(FormaPagtoSelecionado, out FormaPagtoEnum formaPagtoEnum);
            var previsaoInicioFormatada = PrevisaoInicio.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!ValidarCampos(out string mensagemErro))
            {
                await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "Ok");
                return false;
            }
            var idCliente = Preferences.Get("id", string.Empty);
            var novaproposta = new PropostaCreateDTO
            {
                IdEmpresaCliente = int.Parse(idCliente),
                IdPrestador = this.IdPrestador,
                ValorProposta = this.ValorProposto,
                PrevisaoInicio = previsaoInicioFormatada,
                NvlUrgenciaEnum = urgenciaEnum,
                FormaPagtoEnum = formaPagtoEnum,
                StatusServicoEnum = StatusServicoEnum.ORCAMENTO
            };

            var propostaRegistrado = await sService.EnviarPropostaAsync(novaproposta);

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
            if (string.IsNullOrWhiteSpace(TituloProposta) ||
                 string.IsNullOrWhiteSpace(DescProposta) ||
                 ValorProposto == 0 ||
                 PrevisaoInicio == default ||
                 string.IsNullOrWhiteSpace(UrgenciaSelecionada) ||
                 string.IsNullOrWhiteSpace(FormaPagtoSelecionado))
            {
                mensagemErro = "Por favor, preencha todos os campos nescessários antes de criar a solicitação!";
                return false;
            }
            mensagemErro = string.Empty;
            return true;

        }

    }
}
