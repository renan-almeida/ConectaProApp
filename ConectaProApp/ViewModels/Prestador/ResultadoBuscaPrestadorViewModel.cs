using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConectaProApp.ViewModels.Servico;
using ServicoModel = ConectaProApp.Models.Servico; // Alias para a classe Servico

namespace ConectaProApp.ViewModels.Prestador
{
    public class ResultadoBuscaPrestadorViewModel : INotifyPropertyChanged
    {
        private readonly ServicoModel servico; // Usando o alias ServicoModel

        public ObservableCollection<ServicoViewModel> Servicos { get; } // Corrigido para evitar duplicidade

        public ICommand VerMaisCommand { get; set; }

        public ResultadoBuscaPrestadorViewModel(List<ServicoModel> servicos) // Usando o alias ServicoModel
        {
            Servicos = new ObservableCollection<ServicoViewModel>(
                servicos.Select(s => new ServicoViewModel(s))
            );
        }

        private void AtivarVerMais()
        {
            MostrarDescricao = !MostrarDescricao;
        }

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

        // Evento que será disparado para notificar a interface sobre a alteração
        public event PropertyChangedEventHandler PropertyChanged;

        // Método para disparar o evento PropertyChanged
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}