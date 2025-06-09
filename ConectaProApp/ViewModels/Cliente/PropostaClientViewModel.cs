using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
            pService = new PrestadorService();
            NiveisUrgencia = new ObservableCollection<string>(
                 Enum.GetValues(typeof(NvlUrgenciaEnum))
        .Cast<NvlUrgenciaEnum>()
        .Select(e => GetDescription(e))
);

            FormasPagto = new ObservableCollection<string>(
                Enum.GetValues(typeof(FormaPagtoEnum))
                    .Cast<FormaPagtoEnum>()
                    .Select(e => GetDescription(e))
            );
            FinalizarPropostaCommand = new Command(async () => await FinalizarProposta());
            IdPrestador = idPrestador;
            _ = CarregarPrestador();
        }

        private string GetDescription(Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attr = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                             .FirstOrDefault() as DescriptionAttribute;
            return attr?.Description ?? value.ToString();
        }

        private TEnum GetEnumByDescription<TEnum>(string description) where TEnum : struct, Enum
        {
            foreach (var field in typeof(TEnum).GetFields())
            {
                var attr = field.GetCustomAttribute<DescriptionAttribute>();
                if (attr?.Description == description)
                {
                    return (TEnum)field.GetValue(null);
                }
            }

            // fallback: tenta parsear pelo nome do enum (ex: "POUCO_URGENTE")
            if (Enum.TryParse(description, ignoreCase: true, out TEnum result))
            {
                return result;
            }

            throw new ArgumentException($"Valor '{description}' não corresponde a nenhuma descrição ou nome do enum {typeof(TEnum).Name}.");
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


            var urgenciaEnum = GetEnumByDescription<NvlUrgenciaEnum>(UrgenciaSelecionada);
            var formaPagtoEnum = GetEnumByDescription<FormaPagtoEnum>(FormaPagtoSelecionado);
            var previsaoInicioFormatada = PrevisaoInicio.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            if (!ValidarCampos(out string mensagemErro))
            {
                await Application.Current.MainPage.DisplayAlert("Erro", mensagemErro, "Ok");
                return false;
            }
            var novaproposta = new PropostaCreateDTO
            {
                TituloProposta = this.TituloProposta,
                DescProposta = this.DescProposta,
                IdPrestador = this.IdPrestador,
                ValorProposta = this.ValorProposto,
                PrevisaoInicio = previsaoInicioFormatada,
                NvlUrgenciaEnum = urgenciaEnum,
                FormaPagtoEnum = formaPagtoEnum
            };

            var propostaRegistrado = await sService.EnviarPropostaAsync(novaproposta);

            if (propostaRegistrado != null)
            {
                await Application.Current.MainPage
                    .DisplayAlert("Proposta criada com sucesso!",
                                    $"Aguarde o contato do {NomePrestador}", "OK");

                await Task.Delay(1500);

                await Shell.Current.GoToAsync("//cliente");
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
