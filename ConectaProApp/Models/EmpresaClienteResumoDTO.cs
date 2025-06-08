using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConectaProApp.Models
{
    public class EmpresaClienteResumoDTO
    {
        [JsonProperty("idEmpresaCliente")]
        public int IdEmpresaCliente { get; set; }

        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }

        [JsonProperty("razaoSocial")]
        public string RazaoSocial { get; set; }

        [JsonProperty("nomeFantasia")]
        public string NomeFantasia { get; set; }

        [JsonProperty("caminhoFoto")]
        public string CaminhoFoto { get; set; }
    }
}
