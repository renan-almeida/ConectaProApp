using System;
using System.Text.Json.Serialization;
using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;
using JsonIgnoreAttribute = System.Text.Json.Serialization.JsonIgnoreAttribute;

namespace ConectaProApp.Models
{
    public class SolicitacaoDTO
    {
        public int IdSolicitacao { get; set; }
        public string TituloSolicitacao { get; set; }
        public string DescSolicitacao { get; set; }

        public PrestadorResumoDTO PrestadorResumoDTO { get; set; }
        public EmpresaClienteResumoDTO EmpresaClienteResumoDTO { get; set; }

        public decimal ValorProposto { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime PrevisaoInicio { get; set; }

        public int DuracaoServico { get; set; }

        public FormaPagtoEnum FormaPagto { get; set; }
        public NvlUrgenciaEnum NvlUrgencia { get; set; }
        public TipoSegmentoEnum TipoCategoria { get; set; }
        public StatusOrcamentoEnum StatusSolicitacao { get; set; }

        public int IdCliente { get; set; }
        public int IdPrestador { get; set; }
        public string Descricao { get; set; }

        [JsonIgnore]
        public string ValorPropostoFormatado => ValorProposto.ToString("N2");

        [JsonIgnore]
        public string DataInclusaoFormatada => DataInclusao.ToString("dd/MM/yyyy - HH:mm");

        [JsonIgnore]
        public string PrevisaoInicioFormatada => PrevisaoInicio.ToString("dd/MM/yyyy");
    }
}
