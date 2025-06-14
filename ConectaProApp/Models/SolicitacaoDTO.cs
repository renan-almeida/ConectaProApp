using System;
using System.Globalization;
using Newtonsoft.Json;
using ConectaProApp.Models.Enuns;
using ConectaProApp.Converters;

namespace ConectaProApp.Models
{
    public class SolicitacaoDTO
    {
        [JsonProperty("idSolicitacao")]
        public int IdSolicitacao { get; set; }

        [JsonProperty("tituloSolicitacao")]
        public string TituloSolicitacao { get; set; }

        [JsonProperty("descSolicitacao")]
        public string DescSolicitacao { get; set; }

        [JsonProperty("prestadorResumoDTO")]
        public PrestadorResumoDTO? PrestadorResumoDTO { get; set; }

        [JsonProperty("empresaClienteResumoDTO")]
        public EmpresaClienteResumoDTO EmpresaClienteResumoDTO { get; set; }

        [JsonProperty("valorProposto")]
        public float? ValorProposto { get; set; } // deixei nullable pra evitar problemas

        [JsonProperty("dataInclusao")]
        [JsonConverter(typeof(DateTimeFromStringNewtonsoftConverter), "dd/MM/yyyy - HH:mm")]
        public DateTime DataInclusao { get; set; }

        [JsonProperty("previsaoInicio")]
        [JsonConverter(typeof(DateTimeFromStringNewtonsoftConverter), "dd/MM/yyyy")]
        public DateTime PrevisaoInicio { get; set; }

        [JsonProperty("duracaoServico")]
        public int DuracaoServico { get; set; }

        [JsonProperty("formaPagto")]
        [JsonConverter(typeof(SafeStringEnumConverter<FormaPagtoEnum>))]
        public FormaPagtoEnum FormaPagto { get; set; }

        [JsonProperty("nvlUrgencia")]
        [JsonConverter(typeof(SafeStringEnumConverter<NvlUrgenciaEnum>))]
        public NvlUrgenciaEnum NvlUrgencia { get; set; }

        [JsonProperty("tipoCategoria")]
        [JsonConverter(typeof(SafeStringEnumConverter<TipoSegmentoEnum>))]
        public TipoSegmentoEnum TipoCategoria { get; set; }

        [JsonProperty("statusSolicitacao")]
        [JsonConverter(typeof(SafeStringEnumConverter<StatusOrcamentoEnum>))]
        public StatusOrcamentoEnum StatusSolicitacao { get; set; }

        // Propriedades auxiliares

        [JsonIgnore]
        public string ValorPropostoFormatado => ValorProposto?.ToString("N2", CultureInfo.GetCultureInfo("pt-BR")) ?? "0,00";

        [JsonIgnore]
        public string DataInclusaoFormatada => DataInclusao.ToString("dd/MM/yyyy - HH:mm");

        [JsonIgnore]
        public string PrevisaoInicioFormatada => PrevisaoInicio.ToString("dd/MM/yyyy");
    }
}
