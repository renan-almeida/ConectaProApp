using ConectaProApp.Models.Enuns;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ConectaProApp.Services;
using ConectaProApp.Models;
using ConectaProApp.Services.Solicitacao;
using ConectaProApp.ViewModels;


namespace ConectaProApp.ViewModels.Solicitacaos
{
    public class SolicitacaoViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();
        private readonly SolicitacaoService _solicitacaoService;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Descricao { get; set; }
        public decimal ValorProposto { get; set; }
        public int IdCliente { get; set; }
        public int IdPrestador { get; set; }
        public int IdSolicitacao { get; set; }
        public int IdServico { get; set; } // ID do serviço associado à proposta

        private string _abaAtual;
        public string AbaAtual
        {
            get => _abaAtual;
            set
            {
                if (_abaAtual != value)
                {
                    _abaAtual = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<SolicitacaoDTO> PropostasCliente { get; set; } = new();
        public ObservableCollection<SolicitacaoDTO> HistoricoCliente { get; set; } = new();
        public ObservableCollection<SolicitacaoDTO> SolicitacoesRecebidas { get; set; } = new();
        public ObservableCollection<SolicitacaoDTO> ServicosAtivosPrestador { get; set; } = new();
        public ObservableCollection<SolicitacaoDTO> SolicitacoesRecusadas { get; set; } = new();

        public ICommand EnviarPropostaCommand { get; }
        public ICommand AceitarSolicitacaoCommand { get; }
        public ICommand RecusarSolicitacaoCommand { get; }
        public ICommand FinalizarSolicitacaoCommand { get; }
        public ICommand ReofertarSolicitacaoCommand { get; }
        public ICommand SelecionarAbaCommand { get; }
        public ICommand RemoverSolicitacaoCommand { get; }

        public SolicitacaoViewModel()
        {
            _solicitacaoService = new SolicitacaoService(new HttpClient(), new ApiService());

            EnviarPropostaCommand = new Command<int>(async (idSolicitacao) => await EnviarPropostaAsync(idSolicitacao));
            AceitarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.ACEITO));
            RecusarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.RECUSADO));
            FinalizarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.FINALIZADO));
            ReofertarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.PENDENTE));
            RemoverSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await RemoverSolicitacaoAsync(idSolicitacao));
            SelecionarAbaCommand = new Command<string>(aba => AbaAtual = aba);
        }

        private async Task EnviarPropostaAsync(int idSolicitacao)
        {
            try
            {
                var proposta = new SolicitacaoDTO
                {
                    IdCliente = IdCliente,
                    IdPrestador = IdPrestador,
                    IdSolicitacao = idSolicitacao, // Passando o ID da solicitação corretamente
                    Descricao = Descricao,
                    ValorProposto = ValorProposto
                };

                await _solicitacaoService.EnviarPropostaAsync(idSolicitacao, proposta);
                await App.Current.MainPage.DisplayAlert("Sucesso", "Proposta enviada com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", $"Falha ao enviar proposta: {ex.Message}", "OK");
            }
        }

        private async Task AtualizarStatusAsync(int idSolicitacao, int idServico, StatusOrcamentoEnum novoStatus)
        {
            try
            {
                await _solicitacaoService.AtualizarStatusSolicitacaoAsync(idSolicitacao, idServico, novoStatus);
                await App.Current.MainPage.DisplayAlert("Sucesso", "Status atualizado com sucesso", "OK");

                // Atualiza após mudança de status
                await CarregarSolicitacoesPrestador(IdPrestador);
                await CarregarPropostasCliente(IdCliente);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao atualizar status: {ex.Message}", "OK");
            }
        }

        public async Task RemoverSolicitacaoAsync(int idSolicitacao)
        {
            bool confirmacao = await App.Current.MainPage.DisplayAlert("Confirmar", "Deseja remover esta solicitação?", "Sim", "Não");
            if (!confirmacao) return;

            var sucesso = await _solicitacaoService.RemoverSolicitacaoAsync(idSolicitacao);
            if (sucesso)
            {
                await App.Current.MainPage.DisplayAlert("Sucesso", "Solicitação removida com sucesso!", "OK");
                await CarregarPropostasCliente(IdCliente);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Erro ao remover a solicitação.", "OK");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public async Task CarregarPropostasCliente(int idCliente)
        {
            PropostasCliente.Clear();
            HistoricoCliente.Clear();

            var propostas = await _solicitacaoService.BuscarSolicitacoesPorClienteAsync(idCliente);
            foreach (var p in propostas)
            {
                if (p.StatusOrcamentoEnum == StatusOrcamentoEnum.ACEITO || p.StatusOrcamentoEnum == StatusOrcamentoEnum.FINALIZADO)
                    HistoricoCliente.Add(p);
                else
                    PropostasCliente.Add(p);
            }
        }

        public async Task CarregarSolicitacoesPrestador(int idPrestador)
        {
            SolicitacoesRecebidas.Clear();
            ServicosAtivosPrestador.Clear();
            SolicitacoesRecusadas.Clear();

            var solicitacoes = await _solicitacaoService.BuscarSolicitacoesPorPrestadorAsync(idPrestador);
            foreach (var o in solicitacoes)
            {
                switch (o.StatusOrcamentoEnum)
                {
                    case StatusOrcamentoEnum.PENDENTE:
                        SolicitacoesRecebidas.Add(o);
                        break;
                    case StatusOrcamentoEnum.ACEITO:
                        ServicosAtivosPrestador.Add(o);
                        break;
                    case StatusOrcamentoEnum.RECUSADO:
                        SolicitacoesRecusadas.Add(o);
                        break;
                }
            }
        }
    }

}
