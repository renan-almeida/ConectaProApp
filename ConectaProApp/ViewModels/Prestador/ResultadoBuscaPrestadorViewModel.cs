using ConectaProApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConectaProApp.ViewModels.Prestador
{
    public class ResultadoBuscaPrestadorViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Servico> Servicos { get; set; }
        public ICommand VerMaisCommand { get; set; }

        public ResultadoBuscaPrestadorViewModel(List<Servico> servicos)
        {
            Servicos = new ObservableCollection<Servico>(servicos);
            VerMaisCommand = new Command(AtivarVerMais);
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

        // Metódo para disparar o evento propertyChanged
        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
