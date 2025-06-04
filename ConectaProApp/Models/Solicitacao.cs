using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConectaProApp.Models.Enuns;

namespace ConectaProApp.Models
{
    public class Solicitacao
    {
        public int IdSolicitacao { get; set; }
        public string TituloSolicitacao { get; set; }
        public string DescSolicitacao { get; set; }

        public List<Servico> Servicos { get; set; }

        public Usuario IdUsuario { get; set; }
        public EmpresaCliente IdEmpresaCliente { get; set; }
        public Prestador IdPrestador { get; set; }

        [JsonPropertyName("dataInclusao")]
        public DateTime DataInclusao { get; set; }

        public FormaPagtoEnum FormaPagto { get; set; }
        public decimal ValorProposto { get; set; }

        [JsonPropertyName("previsaoInicio")]
        public DateTime PrevisaoInicio { get; set; }

        public int DuracaoServico { get; set; }

        public NvlUrgenciaEnum NvlUrgencia { get; set; }
        public TipoSegmentoEnum TipoCategoria { get; set; }
        public StatusOrcamentoEnum StatusSolicitacao { get; set; }

        // Propriedades auxiliares (opcional para exibição formatada)
        [JsonIgnore]
        public string ValorPropostoFormatado => ValorProposto.ToString("N2");

        [JsonIgnore]
        public string DataInclusaoFormatada => DataInclusao.ToString("dd/MM/yyyy - HH:mm");

        [JsonIgnore]
        public string PrevisaoInicioFormatada => PrevisaoInicio.ToString("dd/MM/yyyy");
    }
}
