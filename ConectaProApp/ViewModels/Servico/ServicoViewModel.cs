using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ServicoModel = ConectaProApp.Models.Servico; // Alias para a classe Servico

namespace ConectaProApp.ViewModels.Servico
{
    public class ServicoViewModel : INotifyPropertyChanged
    {
        private readonly ServicoModel servico; // Usando o alias ServicoModel

        public ServicoViewModel(ServicoModel servico)
        {
            this.servico = servico;
            VerMaisCommand = new Command(() => MostrarDescricao = !MostrarDescricao);
        }

        public string Nome => servico.IdPrestador?.Nome;
        public string StServico => servico.SituacaoServico.ToString();
        public string ValorServico => servico.ValorContratacao.ToString("C");
        public string NvlUrgenciaEnum => servico.NvlUrgenciaEnum.ToString();
        public string NomeSegmento => servico.IdSegmento?.DescSegmento; // Corrigido para DescSegmento
        public string FormaPagamento => servico.FormaPagamento.ToString();
        public string Descricao => servico.Descricao;
        public string Especialidade => servico.IdPrestador?.Especialidades?.FirstOrDefault(); // Ajustado para pegar a primeira especialidade
        public string Logradouro => servico.Logradouro;
        public string Nro => servico.NroEmpresa.ToString();

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

        public ICommand VerMaisCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}