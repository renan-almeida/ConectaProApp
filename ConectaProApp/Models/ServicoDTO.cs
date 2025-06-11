using ConectaProApp.Converters;
using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;

namespace ConectaProApp.Models
{
    public class ServicoDTO
    {
        public int IdServico { get; set; }

        public PrestadorResumoDTO PrestadorResumoDTO { get; set; }
        public EmpresaClienteResumoDTO EmpresaClienteResumoDTO { get; set; }

        [JsonProperty("tituloServico")]
        public string TituloServico { get; set; }

        [JsonProperty("descServico")]
        public string DescServico { get; set; }

        [JsonProperty("valorContratacao")]
        public float ValorContratacao { get; set; }

        [JsonProperty("formaPagtoEnum")]
        public FormaPagtoEnum FormaPagtoEnum { get; set; }

        [JsonProperty("dataInclusao")]
        public DateTime DataInclusao { get; set; }

        [JsonProperty("dataAprovacao")]
        public DateTime? DataAprovacao { get; set; }

        [JsonProperty("dataExecucao")]
        public DateTime? DataExecucao { get; set; }

        [JsonProperty("dataFinalizacao")]
        public DateTime? DataFinalizacao { get; set; }

        [JsonProperty("dataPagmmento")]
        public DateTime? DataPagamento { get; set; }

        [JsonProperty("situacaoServico")]
        public StatusServicoEnum SituacaoServico { get; set; }

        [JsonProperty("nvlUrgenciaEnum")]
        public NvlUrgenciaEnum NvlUrgenciaEnum { get; set; }

        [JsonProperty("tipoCategoriaEnum")]
        public TipoSegmentoEnum TipoCategoriaEnum { get; set; }

        [JsonProperty("previsaoInicio")]
        public string PrevisaoInicio { get; set; }

        [JsonProperty("duracaoServico")]
        public int DuracaoServico { get; set; }

        // Propriedades auxiliares formatadas para exibição
        /*
        [System.Text.Json.Serialization.JsonIgnore]
        public string ValorContratacaoFormatado => ValorContratacao.ToString("N2");

        [System.Text.Json.Serialization.JsonIgnore]
        public string DataInclusaoFormatada => DataInclusao.ToString("dd/MM/yyyy - HH:mm");

        [System.Text.Json.Serialization.JsonIgnore]
        public string DataAprovacaoFormatada => DataAprovacao?.ToString("dd/MM/yyyy - HH:mm");

        [System.Text.Json.Serialization.JsonIgnore]
        public string DataExecucaoFormatada => DataExecucao?.ToString("dd/MM/yyyy - HH:mm");

        [System.Text.Json.Serialization.JsonIgnore]
        public string DataFinalizacaoFormatada => DataFinalizacao?.ToString("dd/MM/yyyy - HH:mm");

        [System.Text.Json.Serialization.JsonIgnore]
        public string DataPagamentoFormatada => DataPagamento?.ToString("dd/MM/yyyy - HH:mm");

        [System.Text.Json.Serialization.JsonIgnore]
        public string PrevisaoInicioFormatada => PrevisaoInicio.ToString("dd/MM/yyyy");

        */
        public string StatusAmigavel => SituacaoServico switch
        {
            StatusServicoEnum.PENDENTE_PAGTO => "Pagamento pendente",
            StatusServicoEnum.PENDENTE_INICIO => "Aguardando início",
            StatusServicoEnum.EM_EXECUCAO => "Em execução",
            StatusServicoEnum.PENDENTE_CONFIRMAR_FINALIZACAO=> "Aguardando confirmação",
            StatusServicoEnum.FINALIZADO => "Finalizado",
            StatusServicoEnum.Agendado => "Agendado",
            StatusServicoEnum.Cancelado => "Cancelado",
            _ => "Indefinido"
        };
    }
}
