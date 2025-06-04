using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class PrestadorResponseBuscaDTO
    {
        [JsonProperty("id")]
        public int IdPrestador { get; set; }
        [JsonProperty("caminhoFoto")]
        public string CaminhoFoto { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("tipoCategoria")]
        public List<string> TipoCategoria { get; set; }
        [JsonProperty("especialidades")]
        public List<string> Especialidades { get; set; }
        [JsonProperty("descPrestador")]
        public string DescPrestador { get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
    }
}
