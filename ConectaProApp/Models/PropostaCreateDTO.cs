
using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class PropostaCreateDTO
    {
        [JsonProperty("idEmpresaCliente")]
        public int IdEmpresaCliente { get; set; }
        [JsonProperty("idPrestador")]
        public int IdPrestador { get; set; }
        [JsonProperty("valorProposto")]
        public decimal ValorProposta { get; set; }
        [JsonProperty("previsaoInicio")]
        public string PrevisaoInicio { get; set; }
        [JsonProperty("formaPagto")]
        public FormaPagtoEnum? FormaPagtoEnum { get; set; }
        [JsonProperty("nvlUrgenciaEnum")]
        public NvlUrgenciaEnum? NvlUrgenciaEnum { get; set; }
        [JsonProperty("StatusServicoEnum")]
        public StatusServicoEnum? StatusServicoEnum { get; set; }
        [JsonProperty("previsaoFim")]
        public string PrevisaoFim { get; set; }

       [JsonProperty("empresaClienteResumoDTO")]
       public EmpresaClienteResumoDTO EmpresaClienteResumoDTO { get; set; }
        [JsonProperty("tituloProposta")]
        public string TituloProposta { get; set; }
        [JsonProperty("descProposta")]
        public string DescProposta { get; set; }

    }
}
