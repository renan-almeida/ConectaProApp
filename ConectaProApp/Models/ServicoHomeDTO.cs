using ConectaProApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ConectaProApp.Models
{
    public class ServicoHomeDTO: BaseViewModel
    {
        [JsonProperty("idSolicitacao")]
        public int IdSolicitacao { get; set; }

        [JsonProperty("tituloSolicitacao")]
        public string? TituloSolicitacao { get; set; }

        [JsonProperty("descSolicitacao")]
        public string? DescSolicitacao { get; set; }

        [JsonProperty("valorProposto")]
        public decimal? ValorProposto { get; set; }

        [JsonProperty("dataInclusao")]
        public string? DataInclusao { get; set; }

        [JsonProperty("previsaoInicio")]
        public string? PrevisaoInicio { get; set; }

        [JsonProperty("duracaoServico")]
        public int? DuracaoServico { get; set; }

        [JsonProperty("formaPagto")]
        public string? FormaPagto { get; set; }

        [JsonProperty("nvlUrgencia")]
        public string? NvlUrgencia { get; set; }

        [JsonProperty("tipoCategoria")]
        public string? TipoCategoria { get; set; }

        [JsonProperty("statusSolicitacao")]
        public string? StatusSolicitacao { get; set; }

        // Propriedades de empresaClienteResumoDTO
        [JsonProperty("empresaClienteResumoDTO")]
        public EmpresaClienteResumoDTO EmpresaClienteResumoDTO { get; set; }

        // Propriedades de prestadorResumoDTO
        [JsonProperty("prestadorResumoDTO")]
        public PrestadorResumoDTO PrestadorResumoDTO { get; set; }
    }

    public class EmpresaClienteResumoDTO: BaseViewModel
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

    public class PrestadorResumoDTO
    {
        [JsonProperty("idPrestador")]
        public int IdPrestador { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("especialidades")]
        public List<string> Especialidades { get; set; }

        [JsonProperty("statusDisponibilidade")]
        public string StatusDisponibilidade { get; set; }

        [JsonProperty("caminhoFoto")]
        public string CaminhoFoto { get; set; }
    }
}
