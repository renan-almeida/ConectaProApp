using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
     public  class SolicitacaoDTO
    {
        
        public int IdSolicitacao { get; set; }
        public string TituloSolicitacao { get; set; }
        public string DescSolicitacao { get; set; }

        public PrestadorResumoDTO PrestadorResumoDTO { get; set; }
        public EmpresaClienteResumoDTO EmpresaClienteResumoDTO { get; set; }

        public decimal ValorProposto { get; set; }

        [JsonPropertyName("dataInclusao")]
        public DateTime DataInclusao { get; set; }

        [JsonPropertyName("previsaoInicio")]
        public DateTime PrevisaoInicio { get; set; }

        public int DuracaoServico { get; set; }

        public FormaPagtoEnum FormaPagto { get; set; }
        public NvlUrgenciaEnum NvlUrgencia { get; set; }
        public TipoSegmentoEnum TipoCategoria { get; set; }
        public StatusOrcamentoEnum StatusSolicitacao { get; set; }

        // Se quiser formatar datas e valores para exibição no front, adicione propriedades auxiliares:
        [JsonIgnore]
        public string ValorPropostoFormatado => ValorProposto.ToString("N2"); // Ex: 1.234,56

        [JsonIgnore]
        public string DataInclusaoFormatada => DataInclusao.ToString("dd/MM/yyyy - HH:mm");

        [JsonIgnore]
        public string PrevisaoInicioFormatada => PrevisaoInicio.ToString("dd/MM/yyyy");

        public int IdCliente { get; set; }
        public int IdPrestador { get; set; }
        public string Descricao { get; set; }
    }
}
