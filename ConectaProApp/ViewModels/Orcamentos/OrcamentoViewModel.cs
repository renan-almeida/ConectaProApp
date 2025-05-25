using ConectaProApp.Models;
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
using ConectaProApp.Services.Orcamento;
using ConectaProApp.ViewModels;
using Microsoft.Maui.Storage;


namespace ConectaProApp.ViewModels.Orcamentos
{
    public class OrcamentoViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();
        private readonly OrcamentoService _orcamentoService;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Descricao { get; set; }
        public decimal ValorProposto { get; set; }
        public int IdCliente { get; set; }
        public int IdPrestador { get; set; }

        public ObservableCollection<OrcamentoDTO> PropostasCliente { get; set; } = new();
        public ObservableCollection<OrcamentoDTO> HistoricoCliente { get; set; } = new();
        public ObservableCollection<OrcamentoDTO> OrcamentosRecebidos { get; set; } = new();
        public ObservableCollection<OrcamentoDTO> ServicosAtivosPrestador { get; set; } = new();
        public ObservableCollection<OrcamentoDTO> OrcamentosRecusados { get; set; } = new();

        public ICommand EnviarPropostaCommand { get; }
        public ICommand AceitarOrcamentoCommand { get; }
        public ICommand RecusarOrcamentoCommand { get; }
        public ICommand FinalizarOrcamentoCommand { get; }
        public ICommand ReofertarOrcamentoCommand { get; }
        public ICommand SelecionarAbaCommand { get; }

        public ICommand RemoverSolicitacaoCommand { get; }



        public OrcamentoViewModel()
        {
            _orcamentoService = new OrcamentoService(new HttpClient(), new ApiService());

            EnviarPropostaCommand = new Command(async () => await EnviarPropostaAsync());
            AceitarOrcamentoCommand = new Command<int>(async (id) => await AtualizarStatusAsync(id, StatusOrcamentoEnum.ACEITO));
            RecusarOrcamentoCommand = new Command<int>(async (id) => await AtualizarStatusAsync(id, StatusOrcamentoEnum.RECUSADO));
            FinalizarOrcamentoCommand = new Command<int>(async (id) => await AtualizarStatusAsync(id, StatusOrcamentoEnum.FINALIZADO));
            ReofertarOrcamentoCommand = new Command<int>(async (id) => await AtualizarStatusAsync(id, StatusOrcamentoEnum.PENDENTE));
            RemoverSolicitacaoCommand = new Command<int>(async (id) => await RemoverSolicitacaoAsync(id));
            SelecionarAbaCommand = new Command<string>(aba => AbaAtual = aba);
        }

        // 🟦 Propriedade de controle de abas
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
                    OnPropertyChanged(nameof(PropostasClienteVisivel));
                    OnPropertyChanged(nameof(HistoricoClienteVisivel));
                    OnPropertyChanged(nameof(ServicosPrestadorVisivel));
                    OnPropertyChanged(nameof(CandidaturasPrestadorVisivel));
                    OnPropertyChanged(nameof(PropostasPrestadorVisivel));
                }
            }
        }

        // 🟨 Visibilidade das abas (bindings para XAML)
        public bool PropostasClienteVisivel => AbaAtual == "PropostasCliente";
        public bool HistoricoClienteVisivel => AbaAtual == "HistoricoCliente";
        public bool ServicosPrestadorVisivel => AbaAtual == "ServicosPrestador";
        public bool CandidaturasPrestadorVisivel => AbaAtual == "CandidaturasPrestador";
        public bool PropostasPrestadorVisivel => AbaAtual == "PropostasPrestador";

        public async Task InicializarClienteAsync(int idCliente)
        {
            IdCliente = idCliente;
            await CarregarPropostasCliente(idCliente);
            AbaAtual = "PropostasCliente";
        }

        public async Task InicializarPrestadorAsync(int idPrestador)
        {
            IdPrestador = idPrestador;
            await CarregarOrcamentosPrestador(idPrestador);
            AbaAtual = "PropostasPrestador";
        }

        private async Task EnviarPropostaAsync()
        {
            try
            {
                var dto = new OrcamentoCreateDTO
                {
                    IdCliente = IdCliente,
                    IdPrestador = IdPrestador,
                    Descricao = Descricao,
                    ValorProposto = ValorProposto
                };

                var orcamento = await _orcamentoService.CriarOrcamentoAsync(dto);
                await App.Current.MainPage.DisplayAlert("Sucesso", "Proposta enviada com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", $"Falha ao enviar proposta: {ex.Message}", "OK");
            }
        }

        public async Task CarregarPropostasCliente(int idCliente)
        {
            PropostasCliente.Clear();
            HistoricoCliente.Clear();

            var propostas = await _orcamentoService.BuscarOrcamentosPorClienteAsync(idCliente);
            foreach (var p in propostas)
            {
                if (p.StatusOrcamentoEnum == StatusOrcamentoEnum.ACEITO || p.StatusOrcamentoEnum == StatusOrcamentoEnum.FINALIZADO)
                    HistoricoCliente.Add(p);
                else
                    PropostasCliente.Add(p);
            }
        }

        public async Task CarregarOrcamentosPrestador(int idPrestador)
        {
            OrcamentosRecebidos.Clear();
            ServicosAtivosPrestador.Clear();
            OrcamentosRecusados.Clear();

            var orcamentos = await _orcamentoService.BuscarOrcamentosPorPrestadorAsync(idPrestador);
            foreach (var o in orcamentos)
            {
                switch (o.StatusOrcamentoEnum)
                {
                    case StatusOrcamentoEnum.PENDENTE:
                        OrcamentosRecebidos.Add(o);
                        break;
                    case StatusOrcamentoEnum.ACEITO:
                        ServicosAtivosPrestador.Add(o);
                        break;
                    case StatusOrcamentoEnum.RECUSADO:
                        OrcamentosRecusados.Add(o);
                        break;
                }
            }
        }

        private async Task AtualizarStatusAsync(int idOrcamento, StatusOrcamentoEnum novoStatus)
        {
            try
            {
                await _orcamentoService.AtualizarStatusOrcamentoAsync(idOrcamento, novoStatus);
                await App.Current.MainPage.DisplayAlert("Sucesso", "Status atualizado com sucesso", "OK");

                // Atualiza após mudança de status
                await CarregarOrcamentosPrestador(IdPrestador);
                await CarregarPropostasCliente(IdCliente);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao atualizar status: {ex.Message}", "OK");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value))
                return false;

            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public async Task RemoverSolicitacaoAsync(int id)
        {
            bool confirmacao = await Application.Current.MainPage.DisplayAlert("Confirmar", "Deseja remover esta solicitação?", "Sim", "Não");
            if (!confirmacao) return;

            var sucesso = await _apiService.DeleteAsync($"/orcamentos/{id}");
            if (sucesso)
            {
                await Application.Current.MainPage.DisplayAlert("Sucesso", "Solicitação removida com sucesso!", "OK");
                await CarregarPropostasCliente(Preferences.Get("IdCliente", 0));
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Erro ao remover a solicitação.", "OK");
            }
        }
    }
}
