using ConectaProApp.Models.Enuns;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class PrestadorCreateDTO
    {
        public int IdUsuario { get; set; }
        [JsonProperty ("nome")]
        public string Nome { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("cpf")]
        public string Cpf { get; set; }
        [JsonProperty("senha")]
        public string Senha { get; set; }
        [JsonProperty("telefone")]
        public string Telefone { get; set; }
        [JsonProperty("dataNascimento")]
        public DateTime DataNascimento { get; set; }
        [JsonProperty("descPrestador")]
        public string DescPrestador { get; set; }
        [JsonProperty("especialidades")]
        public List<string> Especialidades { get; set; }
        [JsonProperty("habilidades")]
        public List<string> Habilidades { get; set; }
        [JsonProperty("segmentos")]
        public List<int> Segmentos { get; set; }
        [JsonProperty("statusDisponibilidade")]
        public StatusDisponibilidadeEnum StatusDisponibilidade { get; set; }
        [JsonProperty("idPlano")]
        public long IdPlano { get; set; }

        // Endereço achatado
        [JsonProperty("cep")]
        public string Cep { get; set; }
        [JsonProperty("numero")]
        public int Numero { get; set; }
        [JsonProperty("complemento")]
        public string Complemento { get; set; }
        [JsonProperty("uf")]
        public UfEnum Uf { get; set; }
    }
}

