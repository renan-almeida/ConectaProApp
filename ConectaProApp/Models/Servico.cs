using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.Models
{
    public class Servico: INotifyPropertyChanged
    {
        public long IdServico { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Especialidade { get; set; } // Exatamente qual tipo de profissional precisa
        public string Logradouro { get; set; }
        public string CepEmpresa { get; set; }
        public string NroEmpresa { get; set; }
        public long Id_Plano { get; set; } 
        public long Id_Segmento { get; set; }
        public string  NomeSegmento { get; set; }
        public long Id_Prestador { get; set; }
        public Prestador prestador { get; set; }
        public string NomePrestador { get; set; }
        public long Id_Cliente { get; set; }
        public EmpresaCliente cliente { get; set; }
        public string NomeCliente { get; set; }
        public long Id_Endereco { get; set; }
        public float ValorServico { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataConclusao { get; set; }
        public DateTime DataPagamento { get; set; }
        public StatusServicoEnum StServico { get; set; }
        public FormaPagtoEnum FormaPagamento { get; set; }
        public NvlUrgenciaEnum NvlUrgenciaEnum { get; set; }

        
        public Servico(Plano plano, Segmento segmento, Prestador prestador, EmpresaCliente cliente)
        {
            Id_Plano = plano.IdPlano;
            Id_Segmento = segmento.IdSegmento;
            Id_Prestador = prestador.IdPrestador;
            Id_Cliente = cliente.IdEmpresa;
            VerMaisCommand = new Command(AtivarVerMais);
        }

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

