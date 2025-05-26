using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace ConectaProApp.Models
{
    public class Servico
    {
        public int IdServico { get; set; }
        public Prestador IdPrestador { get; set; }
        public Segmento IdSegmento { get; set; }
        public EmpresaCliente IdCliente { get; set; }
        public float ValorContratacao { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAprovacao { get; set; }
        public DateTime DataExecucao { get; set; }
        public DateTime DataPagamento { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Logradouro { get; set; }
        public string CepEmpresa { get; set; }
        public int NroEmpresa { get; set; }
        public StatusServicoEnum SituacaoServico { get; set; }
        public FormaPagtoEnum FormaPagamento { get; set; }
        public NvlUrgenciaEnum NvlUrgenciaEnum { get; set; }
        public string FotoServico { get; set; }

        public int IdSolicitacao { get; set; }

        public StatusOrcamentoEnum StatusOrcamento { get; set; }
           
    }
}

