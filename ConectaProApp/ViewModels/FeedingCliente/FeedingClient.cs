using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ConectaProApp.Models;
using ConectaProApp.Models.Enuns;

namespace ConectaProApp.ViewModels.FeedingCliente
{
    public class FeedingClient : BaseViewModel
    {
        public ObservableCollection<ConectaProApp.Models.Prestador> Prestadores { get; set; }
        public ICommand LoadMoreCommand { get; }

        public FeedingClient()
        {
            Prestadores = new ObservableCollection<ConectaProApp.Models.Prestador>();
            LoadMoreCommand = new Command(async () => await LoadMorePrestadores());
            LoadInitialData();
        }

        private void LoadInitialData()
        {
            for (int i = 1; i <= 10; i++)
            {
                string descricao = $"Descrição do Prestador {i}";
                if (string.IsNullOrWhiteSpace(descricao))
                {
                    throw new ArgumentException("Descrição do Prestador não pode ser vazia.");
                }

                Prestadores.Add(new ConectaProApp.Models.Prestador(new Plano { IdPlano = i })
                {
                    IdPrestador = i,
                    DescPrestador = descricao,
                    Segmento = new List<string> { "Segmento A", "Segmento B" },
                    Especialização = new List<string> { "Especialização X", "Especialização Y" },
                    StatusDisponibilidade = i % 2 == 0 ? StatusDisponibilidadeEnum.DISPONIVEL : StatusDisponibilidadeEnum.INDISPONIVEL
                });
            }
        }

        private async Task LoadMorePrestadores()
        {
            await Task.Delay(1000); // Simula um atraso
            int count = Prestadores.Count;
            for (int i = count + 1; i <= count + 10; i++)
            {
                string descricao = $"Descrição do Prestador {i}";
                if (string.IsNullOrWhiteSpace(descricao))
                {
                    throw new ArgumentException("Descrição do Prestador não pode ser vazia.");
                }

                Prestadores.Add(new ConectaProApp.Models.Prestador(new Plano { IdPlano = i })
                {
                    IdPrestador = i,
                    DescPrestador = descricao,
                    Segmento = new List<string> { "Segmento C", "Segmento D" },
                    Especialização = new List<string> { "Especialização Z", "Especialização W" },
                    StatusDisponibilidade = i % 2 == 0 ? StatusDisponibilidadeEnum.DISPONIVEL : StatusDisponibilidadeEnum.INDISPONIVEL
                });
            }
        }
    }
}