using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class ServicoHomeDTO
    {
        [JsonProperty("idSolicitacao")]
        public int IdSolicitacao { get; set; }
        [JsonProperty("descSolicitacao")]
        public string DescSolicitacao { get; set; }
        [JsonProperty("caminhoFoto")]
        public string CaminhoFoto { get; set; }
        [JsonProperty("nomeFantasia")]
        public string NomeFantasia { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
    }
}
