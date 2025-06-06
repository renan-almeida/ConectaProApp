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
using ConectaProApp.ViewModels.Foto;
using System.Diagnostics;
using ConectaProApp.Services.Azure;
using Newtonsoft.Json;

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
        public int IdServico { get; set; }

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

        private bool _isUploading;
        public bool IsUploading
        {
            get => _isUploading;
            set
            {
                if (_isUploading != value)
                {
                    _isUploading = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _historicoClienteVisivel;
        public bool HistoricoClienteVisivel
        {
            get => _historicoClienteVisivel;
            set
            {
                _historicoClienteVisivel = value;
                OnPropertyChanged();
            }
        }

        private bool _propostasClienteVisivel;
        public bool PropostasClienteVisivel
        {
            get => _propostasClienteVisivel;
            set
            {
                _propostasClienteVisivel = value;
                OnPropertyChanged();
            }
        }

        private bool _solicitacoesClienteVisivel;
        public bool SolicitacoesClienteVisivel
        {
            get => _solicitacoesClienteVisivel;
            set
            {
                _solicitacoesClienteVisivel = value;
                OnPropertyChanged();
            }
        }

        private bool _servicosPrestadorVisivel;
        public bool ServicosPrestadorVisivel
        {
            get => _servicosPrestadorVisivel;
            set
            {
                _servicosPrestadorVisivel = value;
                OnPropertyChanged();
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
        public ICommand SelecionarFotoCommand { get; }

        public FotoViewModel FotoVM { get; }

        public enum TipoUsuario
        {
            Cliente,
            Prestador
        }

        public SolicitacaoViewModel(TipoUsuario tipoUsuario, int idUsuario)
        {


            _solicitacaoService = new SolicitacaoService(new HttpClient(), new ApiService());
            var apiService = new ApiService();
            var blobService = new BlobService(apiService);

            string endpointApi;
            string chavePreferencia;

            if (tipoUsuario == TipoUsuario.Cliente)
            {
                IdCliente = idUsuario;
                endpointApi = $"/clientes/{IdCliente}";
                chavePreferencia = "foto_cliente";
            }
            else
            {
                IdPrestador = idUsuario;
                endpointApi = $"/prestadores/{IdPrestador}";
                chavePreferencia = "foto_prestador";
            }

            FotoVM = new FotoViewModel(blobService, apiService, endpointApi, chavePreferencia);

            EnviarPropostaCommand = new Command<int>(async (idSolicitacao) => await EnviarPropostaAsync(idSolicitacao));
            AceitarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.ACEITA));
            RecusarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.RECUSADA));
            FinalizarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.FINALIZADA));
            ReofertarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.PENDENTE));
            RemoverSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await RemoverSolicitacaoAsync(idSolicitacao));
            SelecionarAbaCommand = new Command<string>(async (aba) => await SelecionarAbaAsync(aba));
            SelecionarFotoCommand = FotoVM.SelecionarFotoCommand;
            _ = SelecionarAbaAsync("HistoricoCliente");
        }

        public SolicitacaoViewModel() : this(TipoUsuario.Cliente, Preferences.Get("id", 0)) { }

        private async Task EnviarPropostaAsync(int idSolicitacao)
        {
            try
            {
                var proposta = new SolicitacaoDTO
                {
                    IdCliente = IdCliente,
                    IdPrestador = IdPrestador,
                    IdSolicitacao = idSolicitacao,
                    Descricao = Descricao,
                    ValorProposto = ValorProposto,
                };

                await _solicitacaoService.EnviarPropostaAsync(idSolicitacao, proposta);
                await Application.Current.MainPage.DisplayAlert("Sucesso", "Proposta enviada com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Falha ao enviar proposta: {ex.Message}", "OK");
            }
        }

        private async Task AtualizarStatusAsync(int idSolicitacao, int idServico, StatusOrcamentoEnum novoStatus)
        {
            try
            {
                await _solicitacaoService.AtualizarStatusSolicitacaoAsync(idSolicitacao, idServico, novoStatus);
                await App.Current.MainPage.DisplayAlert("Sucesso", "Status atualizado com sucesso", "OK");

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

            try
            {
                var propostas = await _solicitacaoService.BuscarSolicitacoesPorClienteAsync(idCliente);
                foreach (var p in propostas)
                {
                    if (p.StatusSolicitacao == StatusOrcamentoEnum.ACEITA || p.StatusSolicitacao == StatusOrcamentoEnum.FINALIZADA)
                        HistoricoCliente.Add(p);
                    else
                        PropostasCliente.Add(p);
                }
            }
            catch (JsonException ex)
            {
                throw new Exception("Erro ao desserializar solicitações do cliente. Verifique o formato da resposta JSON.", ex);
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
                switch (o.StatusSolicitacao)
                {
                    case StatusOrcamentoEnum.PENDENTE:
                        SolicitacoesRecebidas.Add(o);
                        break;
                    case StatusOrcamentoEnum.ACEITA:
                        ServicosAtivosPrestador.Add(o);
                        break;
                    case StatusOrcamentoEnum.RECUSADA:
                        SolicitacoesRecusadas.Add(o);
                        break;
                }
            }
        }

        private async Task SelecionarAbaAsync(string aba)
        {
            HistoricoClienteVisivel = false;
            PropostasClienteVisivel = false;
            SolicitacoesClienteVisivel = false;
            ServicosPrestadorVisivel = false;

            AbaAtual = aba;

            switch (aba)
            {
                case "HistoricoCliente":
                    HistoricoClienteVisivel = true;
                    await CarregarPropostasCliente(IdCliente);
                    break;

                case "PropostasCliente":
                    PropostasClienteVisivel = true;
                    await CarregarPropostasCliente(IdCliente);
                    break;

                case "SolicitacoesCliente":
                    SolicitacoesClienteVisivel = true;
                    break;

                case "ServicosPrestador":
                    ServicosPrestadorVisivel = true;
                    break;
            }
        }
    }

}