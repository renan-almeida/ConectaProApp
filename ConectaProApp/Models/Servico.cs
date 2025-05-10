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
    public class Servico: INotifyPropertyChanged
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

        /* Realizamos esse tratamento abaixo por conta que a classe servicos
         na resultadoBuscaViewModel é uma lista, graças a isso precisamos realizar esse tratamento
        diretamente dentro da models para permitir que cada serviço a ser exibido em tela tenha
        o "Ver mais" disponivel."
        */

        // Propriedade para controlar a visibilidade das informações
        private bool mostrarDescricao;
        public bool MostrarDescricao
        {
            get => mostrarDescricao;
            set
            {
                if (mostrarDescricao != value)
                {
                    mostrarDescricao = value;
                    OnPropertyChanged(nameof(MostrarDescricao));
                }
            }
        }

        // Comando para "Ver mais"
        public ICommand VerMaisCommand { get; set; }

       

        // Método que alterna a visibilidade das informações
        private void AtivarVerMais()
        {
            MostrarDescricao = !MostrarDescricao;
        }

        // Implementação do INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

