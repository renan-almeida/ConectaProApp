

using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Cliente.CriarSolicitacaoViews;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Globalization;

namespace ConectaProApp.ViewModels.Cliente
{
    public class CriarSolicitacaoViewModel : BaseViewModel
    {
        private ServicoService sService;
        public ICommand EtapaDoisCommand { get; set; }
        public ICommand EtapaFinalCommand { get; set; }
        public ICommand FinalizarSolicitacaoCommand { get; set; }

        public CriarSolicitacaoViewModel()
        {
            sService = new ServicoService();
            InitializeCommands();
            Segmentos = [.. Enum.GetNames(typeof(TipoSegmentoEnum))];
            NiveisUrgencia = [.. Enum.GetNames(typeof(NvlUrgenciaEnum))];
            FormasPagto = [.. Enum.GetNames(typeof(FormaPagtoEnum))];
        }
        private string titulo;
        public string Titulo
        {
            get => titulo;
            set
            {
                titulo = value;
                OnPropertyChanged();
            }
        }

        private string descricao;
        public string Descricao
        {
            get => descricao;
            set
            {
                descricao = value;
                OnPropertyChanged();
            }
        }

        private string especiailidade;
        public string Especialidade
        {
            get => especiailidade;
            set
            {
                especiailidade = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> Segmentos { get; set; }
        private string segmentoSelecionado;
        public string SegmentoSelecionado
        {
            get => segmentoSelecionado;
            set
            {
                segmentoSelecionado = value;
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


        private int duracaoServico;
        public int DuracaoServico
        {
            get => duracaoServico;
            set
            {
                duracaoServico = value;
                OnPropertyChanged();
            }
        }

        private int nro;
        public int Nro
        {
            get => nro;
            set
            {
                nro = value;
                OnPropertyChanged();
            }
        }

        private string cep;
        public string Cep
        {
            get => cep;
            set
            {
                cep = MascararCep(RemoverNaoNumericos(value));
                OnPropertyChanged();
            }
        }

        private string MascararCep(string input)
        {
            input = new string(input.Where(char.IsDigit).ToArray());

            if (input.Length > 8)
                input = input.Substring(0, 8);

            if (input.Length < 8)
                return input;

            return Convert.ToUInt64(input).ToString(@"00000\-000");


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
        private string valorPropostoTexto;
        public string ValorPropostoTexto
        {
            get => valorPropostoTexto;
            set
            {
                valorPropostoTexto = value;

                // Remove "R$", espaços, vírgula vira ponto
                var textoLimpo = value?
                    .Replace("R$", "")
                    .Replace(" ", "")
                    .Replace(",", ".")
                    .Trim();

                if (decimal.TryParse(textoLimpo, NumberStyles.Any, CultureInfo.InvariantCulture, out var resultado))
                {
                    ValorProposto = resultado;
                }

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

        private string fotoServico;
        public string FotoServico
        {
            get => fotoServico;
            set
            {
                fotoServico = value;
                OnPropertyChanged();
            }
        }

        private void InitializeCommands()
        {
            EtapaDoisCommand = new Command(async () => await EtapaDois());
            EtapaFinalCommand = new Command(async () => await EtapaFinal());
            FinalizarSolicitacaoCommand = new Command(async () => await FinalizarSolicitacao());
        }

        public async Task EtapaDois()
        {
            try
            {
                var proximaPagina = new CriarSolicitacaoClientTwo
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Erro!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }

        public async Task EtapaFinal()
        {
            try
            {
                var proximaPagina = new CriarSolicitacaoClientFinal
                {
                    BindingContext = this
                };


                await Application.Current.MainPage.Navigation.PushAsync(proximaPagina);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert
                       ("Erro!", ex.Message + "Detalhes: " + ex.InnerException, "Ok");

            }
        }

        public async Task<bool> FinalizarSolicitacao()
        {
      

            Enum.TryParse(UrgenciaSelecionada, out NvlUrgenciaEnum urgenciaEnum);
            Enum.TryParse(FormaPagtoSelecionado, out FormaPagtoEnum formaPagtoEnum);
            Enum.TryParse(SegmentoSelecionado, out TipoSegmentoEnum tipoSegmentoEnum);
            var previsaoInicioFormatada = PrevisaoInicio.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (!ValidarCampos(out string mensagemErro))
            {
                await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "Ok");
                return false;
            }

            var novaSolicitacao = new ServicoCreateDTO
            {
                TituloSolicitacao = this.Titulo,
                DescSolicitacao = this.Descricao,
                ValorProposto = this.ValorProposto,
                TipoCategoriaEnum = tipoSegmentoEnum,
                PrevisaoInicio = previsaoInicioFormatada,
                DuracaoServico = this.DuracaoServico, 
                NvlUrgenciaEnum = urgenciaEnum,
                FormaPagtoEnum = formaPagtoEnum,
            };

            var servicoRegistrado = await sService.PostRegistrarServicoAsync(novaSolicitacao);
           
            if (servicoRegistrado != null)
            {
                Preferences.Set("idSolicitacao", servicoRegistrado.Id);
                await Application.Current.MainPage
                    .DisplayAlert("Solicitação criada com sucesso!",
                                    "Aguarde o contato de um prestador", "OK");

                await  Task.Delay(1500);

                await Shell.Current.GoToAsync("//cliente");
                return true;
            }
            return false; 
        }


        public bool ValidarCampos(out string mensagemErro)
        {
            if (string.IsNullOrWhiteSpace(Titulo) ||
                 string.IsNullOrWhiteSpace(Descricao) ||
                 string.IsNullOrWhiteSpace(SegmentoSelecionado)  ||
                 string.IsNullOrWhiteSpace(UrgenciaSelecionada) ||
                 string.IsNullOrWhiteSpace(FormaPagtoSelecionado))
            {
                mensagemErro = "Por favor, preencha todos os campos nescessários antes de criar a solicitação!";
                return false;
            }

            mensagemErro = string.Empty;
            return true;

        }

        private string RemoverNaoNumericos(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
