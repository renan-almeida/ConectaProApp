using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ConectaProApp.Models.Enuns;

namespace ConectaProApp.Models
{
    public class SolicitacaoUpdateDTO
    {
        public string TituloOrcamento { get; set; }
        public string DescOrcamento { get; set; }
        public decimal ValorOrcamento { get; set; }

        [JsonPropertyName("previsaoInicio")]
        public DateTime PrevisaoInicio { get; set; }

        public int DuracaoServico { get; set; }

        public FormaPagtoEnum FormaPagtoEnum { get; set; }
        public NvlUrgenciaEnum NvlUrgenciaEnum { get; set; }
        public TipoSegmentoEnum TipoCategoriaEnum { get; set; }
        public StatusOrcamentoEnum StatusSolicitacaoEnum { get; set; }

        // Propriedades auxiliares para exibição (opcional)
        [JsonIgnore]
        public string ValorOrcamentoFormatado => ValorOrcamento.ToString("N2");

        [JsonIgnore]
        public string PrevisaoInicioFormatada => PrevisaoInicio.ToString("dd/MM/yyyy");
    }
}
