using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class ServicoCreateDTO
    {
        [JsonProperty("idSolicitacao")]
        public int Id { get; set; }


        [JsonProperty("tituloSolicitacao")]
        public string TituloSolicitacao { get; set; }
        [JsonProperty("descSolicitacao")]
        public string DescSolicitacao { get; set; }
        [JsonProperty("dataInclusao")]
        public string dataInclusao { get; set; }
        [JsonProperty("especialidade")]
        public string Especialidade { get; set; }
        [JsonProperty("tipoCategoriaEnum")]
        public TipoSegmentoEnum TipoCategoriaEnum { get; set; }
        [JsonProperty("valorProposto")]
        public decimal ValorProposto { get; set; }
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty("cep")]
        public string Cep { get; set; }
        [JsonProperty("numero")]
        public int Numero { get; set; }
        [JsonProperty("formaPagtoEnum")]
        public FormaPagtoEnum FormaPagtoEnum { get; set; }
        [JsonProperty("nvlUrgenciaEnum")]
        public NvlUrgenciaEnum NvlUrgenciaEnum { get; set; }
        [JsonProperty("fotoServico")]
        public string FotoServico { get; set; }
        [JsonProperty("previsaoInicio")]
        public string PrevisaoInicio { get; set; }
        [JsonProperty("duracaoServico")]
        public int DuracaoServico { get; set; }
        [JsonProperty("statusSolicitacaoEnum")]
        public StatusOrcamentoEnum StatusSolicitacaoEnum { get; set; }
    }
}
