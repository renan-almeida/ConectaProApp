using System;
using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;

namespace ConectaProApp.Models
{
    public class ServicoDTO
    {
        public int IdServico { get; set; }

        public PrestadorResumoDTO PrestadorResumoDTO { get; set; }
        public EmpresaClienteResumoDTO EmpresaClienteResumoDTO { get; set; }

        public string TituloServico { get; set; }
        public string DescServico { get; set; }

        public float ValorContratacao { get; set; }

        public FormaPagtoEnum FormaPagtoEnum { get; set; }

        public DateTime DataInclusao { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public DateTime? DataExecucao { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public DateTime? DataPagamento { get; set; }

        public StatusServicoEnum SituacaoServico { get; set; }
        public NvlUrgenciaEnum NvlUrgenciaEnum { get; set; }
        public TipoSegmentoEnum TipoCategoriaEnum { get; set; }

        public DateTime PrevisaoInicio { get; set; }

        public int DuracaoServico { get; set; }

        // Propriedades auxiliares formatadas para exibição
        [JsonIgnore]
        public string ValorContratacaoFormatado => ValorContratacao.ToString("N2");

        [JsonIgnore]
        public string DataInclusaoFormatada => DataInclusao.ToString("dd/MM/yyyy - HH:mm");

        [JsonIgnore]
        public string DataAprovacaoFormatada => DataAprovacao?.ToString("dd/MM/yyyy - HH:mm");

        [JsonIgnore]
        public string DataExecucaoFormatada => DataExecucao?.ToString("dd/MM/yyyy - HH:mm");

        [JsonIgnore]
        public string DataFinalizacaoFormatada => DataFinalizacao?.ToString("dd/MM/yyyy - HH:mm");

        [JsonIgnore]
        public string DataPagamentoFormatada => DataPagamento?.ToString("dd/MM/yyyy - HH:mm");

        [JsonIgnore]
        public string PrevisaoInicioFormatada => PrevisaoInicio.ToString("dd/MM/yyyy");

        [JsonIgnore]
        public string StatusAmigavel => SituacaoServico switch
        {
            StatusServicoEnum.PendentePagto => "Pagamento pendente",
            StatusServicoEnum.PendenteInicio => "Aguardando início",
            StatusServicoEnum.EM_EXECUCAO => "Em execução",
            StatusServicoEnum.PendenteConfirmarFinalizacao => "Aguardando confirmação",
            StatusServicoEnum.FINALIZADO => "Finalizado",
            StatusServicoEnum.Agendado => "Agendado",
            StatusServicoEnum.Cancelado => "Cancelado",
            _ => "Indefinido"
        };
    }
}
