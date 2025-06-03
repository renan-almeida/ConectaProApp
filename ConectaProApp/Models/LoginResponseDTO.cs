using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConectaProApp.Models
{
    public class LoginResponseDTO
    {
        [JsonProperty("id")]
        public int Id {  get; set; }
        [JsonProperty("uf")]
        public string Uf { get; set; }
        [JsonProperty("tipoUsuario")]
        public string TipoUsuario { get; set; }
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }

    }
}
