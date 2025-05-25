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
        [JsonProperty("idUsuario")]
        public int Id { get; set; }


        [JsonProperty("titulo")]
        public string Titulo { get; set; }
        [JsonProperty("descricao")]
        public string Descricao { get; set; }
        [JsonProperty("especialidade")]
        public string Especialidade { get; set; }
        [JsonProperty("segmento")]
        public int Segmento { get; set; }
        [JsonProperty("valorContratacao")]
        public float ValorContratacao { get; set; }
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty("cep")]
        public string Cep { get; set; }
        [JsonProperty("numero")]
        public int Numero { get; set; }
        [JsonProperty("formaPagamento")]
        public int FormaPagamento { get; set; }
        [JsonProperty("nivelUrgencia")]
        public int NvlUrgenciaEnum { get; set; }
        [JsonProperty("fotoServico")]
        public string FotoServico { get; set; }

        [JsonProperty("idSolicitacao")]
        public int IdSolicitacao { get; set; }

        [JsonProperty("idPrestador")]
        public int IdPrestador { get; set; }
    }
}
