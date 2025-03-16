using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConectaProApp.Models
{
    public class SolicitacaoServico
    {
        public long idSolicitacao { get; set; }
        private long id_Usuario;
        private string tituloSolicitacao;
        private string descricao;
        private string FotoUrl;
        private StatusSolicitacaoEnum statusSolicitacao;
        private TipoCategoriaEnum categoria;
        private string especialidade;
        private string localidade;
        private NvlUrgenciaEnum nvlUrgencia;
        private string atividades;
        private double valorProposto;
        private DateTime dataCriacao;
        private DateTime dataAtualização;
        private DateTime dataFinalizacao;
        private long id_Avaliacao;
        private FormaPagtoEnum formaPagto;
        private string prazoExecucao;

        public SolicitacaoServico( Usuario usuario, Avaliacao avaliacao)
        {
            id_Usuario = usuario.IdUsuario;
            id_Avaliacao = avaliacao.idAvaliacao;
            
        }
    }
}
