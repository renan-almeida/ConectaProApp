using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class EmpresaCreateDTO
    {
        public int IdUsuario { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("razaoSocial")]
        public string RazaoSocial { get; set; }
        [JsonProperty("nomeFantasia")]
        public string NomeFantasia { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("cnpj")]
        public string Cnpj { get; set; }
        [JsonProperty("telefone")]
        public string Telefone { get; set; }
        [JsonProperty("senha")]
        public string Senha { get; set; }

        // Endereco
        [JsonProperty("cep")]
        public string Cep { get; set; }
        [JsonProperty("numero")]
        public int Numero { get; set; }
        [JsonProperty("complemento")]
        public string Complemento { get; set; }
        [JsonProperty("uf")]
        public UfEnum Uf { get; set; }

        [JsonProperty("caminhoAvatar")]
        public string CaminhoAvatar { get; set; }

        [JsonProperty("caminhoHeader")]
        public string CaminhoHeader { get; set; }


    }
}
