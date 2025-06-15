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
using ConectaProApp.Services.Cliente;
using ConectaProApp.Services.Servico;
using ConectaProApp.Services.Prestador;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ConectaProApp.ViewModels.Solicitacaos
{

    public partial class SolicitacaoViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();
        private readonly SolicitacaoService _solicitacaoService;
        private readonly ServicoService _servicoService;
        private readonly PrestadorService _prestadorService;
        private readonly PerfilEmpresaClienteService _perfilService;
        private readonly PerfilPrestadorService _perfilPrestadorService;
        public event PropertyChangedEventHandler PropertyChanged;

       
        public string TituloServico { get; set; }
        public string Descricao { get; set; }
        public float ValorContratacao { get; set; }
        public int IdCliente { get; set; }
        public int IdPrestador { get; set; }
        public int IdSolicitacao { get; set; }
        public int IdServico { get; set; }
        public string PrevisaoInicio { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime? DataAprovacao { get; set; }
        public DateTime? DataExecucao { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public DateTime? DataPagamento { get; set; }

        public int DuracaoServico { get; set; }

        public PrestadorResumoDTO PrestadorResumoDTO { get; set; }
        public EmpresaClienteResumoDTO EmpresaClienteResumoDTO { get; set; }
        public FormaPagtoEnum FormaPagtoEnum { get; set; }
        public StatusServicoEnum SituacaoServico { get; set; }
        public NvlUrgenciaEnum NvlUrgenciaEnum { get; set; }
        public TipoSegmentoEnum TipoCategoriaEnum { get; set; }

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

        private bool _solicitacaoClienteVisivel;
        public bool SolicitacaoClienteVisivel
        {
            get => _solicitacaoClienteVisivel;
            set
            {
                _solicitacaoClienteVisivel = value;
                Debug.WriteLine($"🔧 SolicitacaoClienteVisivel alterado para: {value}");
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

        private ObservableCollection<ServicoDTO> _propostaCliente;
        public ObservableCollection<ServicoDTO> PropostaCliente
        {
            get => _propostaCliente;
            set
            {
                _propostaCliente = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<ServicoDTO> _historicoCliente;

        public ObservableCollection<ServicoDTO> HistoricoCliente
        {
            get => _historicoCliente;
            set
            {
                _historicoCliente = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<SolicitacaoDTO> SolicitacoesRecebidas { get; set; } = new();

        public ObservableCollection<SolicitacaoDTO> SolicitacaoCliente { get; set; } = new();
        public ObservableCollection<SolicitacaoDTO> ServicosAtivosPrestador { get; set; } = new();
        public ObservableCollection<SolicitacaoDTO> SolicitacoesRecusadas { get; set; } = new();

        public ObservableCollection<SolicitacaoDTO> PropostasRecebidasPrestador { get; set; } = new();

        private bool _propostasPrestadorVisivel;
        public bool PropostasPrestadorVisivel
        {
            get => _propostasPrestadorVisivel;
            set
            {
                _propostasPrestadorVisivel = value;
                OnPropertyChanged();
            }
        }


        public ICommand EnviarPropostaCommand { get; }
        public ICommand AceitarSolicitacaoCommand { get; }
        public ICommand RecusarSolicitacaoCommand { get; }
        public ICommand FinalizarSolicitacaoCommand { get; }
        public ICommand ReofertarSolicitacaoCommand { get; }
        public ICommand SelecionarAbaCommand { get; }
        public ICommand RemoverSolicitacaoCommand { get; }
        public ICommand SelecionarFotoCommand { get; }

        // métodos para serviços exibidos

        //Esse método será realizado pelo cliente ->
        public ICommand PagarServicoCommand { get; }

        //Esse método será realizado pelo prestador ->
        public ICommand IniciarServicoCommand { get; }

        //Esse método será realizado pelo prestador ->
        public ICommand FinalizarServicoCommand { get; }

        //Esse método será realizado pelo cliente ->
        public ICommand ConfirmarFinalizacaoServicoCommand { get; }


        public FotoViewModel FotoVMAvatar { get; }
        public FotoViewModel FotoVMHeader { get; }

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

            _perfilPrestadorService = new PerfilPrestadorService();
            _perfilService = new PerfilEmpresaClienteService();
            HistoricoCliente = new ObservableCollection<ServicoDTO>();
            PropostaCliente = new ObservableCollection<ServicoDTO>();
            SolicitacaoCliente = new ObservableCollection<SolicitacaoDTO>();
            HistoricoClienteVisivel = false;
            PropostasClienteVisivel = false;
            _servicoService = new ServicoService();
            _prestadorService = new PrestadorService();

            string endpointApi;
            string chaveAvatar;
            string chaveHeader;

            if (tipoUsuario == TipoUsuario.Cliente)
            {
                IdCliente = idUsuario;
                endpointApi = $"/clientes/{IdCliente}";
                chaveAvatar = "foto_avatar_cliente";
                chaveHeader = "foto_header_cliente";
            }
            else
            {
                IdPrestador = idUsuario;
                endpointApi = $"/prestadores/{IdPrestador}";
                chaveAvatar = "foto_avatar_prestador";
                chaveHeader = "foto_header_prestador";
            }

            FotoVMAvatar = new FotoViewModel(blobService, apiService, endpointApi, chaveAvatar);
            FotoVMHeader = new FotoViewModel(blobService, apiService, endpointApi, chaveHeader);

            //EnviarPropostaCommand = new Command<int>(async (idSolicitacao) => await EnviarPropostaAsync(idSolicitacao));
            AceitarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.ACEITA));
            RecusarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.RECUSADA));
            FinalizarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.FINALIZADA));
            ReofertarSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await AtualizarStatusAsync(idSolicitacao, IdServico, StatusOrcamentoEnum.PENDENTE));
            RemoverSolicitacaoCommand = new Command<int>(async (idServico) => await RemoverSolicitacaoAsync(idServico));
            PagarServicoCommand = new Command<int>(async (IdServico) => await PagarServicoAsync(IdServico));
            SelecionarAbaCommand = new Command<string>(async (aba) => await SelecionarAbaAsync(aba));
            SelecionarFotoCommand = FotoVMAvatar.SelecionarFotoCommand;

        }

        public SolicitacaoViewModel()
     : this(TipoUsuario.Cliente, Preferences.Get("idCliente", 0)) { }

        public SolicitacaoViewModel(bool isPrestador)
            : this(
                isPrestador ? TipoUsuario.Prestador : TipoUsuario.Cliente,
                isPrestador ? Preferences.Get("idPrestador", 0) : Preferences.Get("idCliente", 0)
              )
        { }

        private string nomePrestador;
        public string NomePrestador
        {
            get => nomePrestador;
            set
            {
                if (nomePrestador != value)
                {
                    nomePrestador = value;
                    OnPropertyChanged();
                }
            }
        }

        private string descricaoPrestador;
        public string DescricaoPrestador
        {
            get => descricaoPrestador;
            set
            {
                if (descricaoPrestador != value)
                {
                    descricaoPrestador = value;
                    OnPropertyChanged();
                }
            }
        }

   
        public async Task CarregarDadosPrestadorAsync()
        {
            try
            {
                string idPrestador = Preferences.Get("id", string.Empty);

                if (string.IsNullOrEmpty(idPrestador))
                    return;

                var prestador = await _prestadorService.BuscarPrestadorPorIdAsync(int.Parse(idPrestador));

                if(prestador != null)
                {
                    NomePrestador = prestador.Nome ?? "sem nome";
                    Debug.WriteLine("Nome: " + NomePrestador);
                    DescricaoPrestador = prestador.DescPrestador ?? "sem descricao";
                    Debug.WriteLine("Desc: " + DescricaoPrestador);
                }
            }
            catch ( Exception ex)
            {
                Debug.WriteLine($"Erro ao carregar dados do prestador: {ex.Message}");
            }
        }

        public async Task CarregarPropostasPrestadorAsync()
        {
            try
            {
                int idPrestador = Preferences.Get("id", 0);
                 // aqui é o equivalente do Cliente
                Debug.WriteLine($"🔧 Buscando propostas para o prestador {idPrestador}");

                var propostas = await _perfilPrestadorService.BuscarPropostasPrestadorAsync(idPrestador);
                Debug.WriteLine($"🔧 Total de propostas recebidas: {propostas.Count}");

                PropostasRecebidasPrestador.Clear();
                foreach (var proposta in propostas)
                    PropostasRecebidasPrestador.Add(proposta);

                PropostasPrestadorVisivel = true;
            }
            catch (Exception ex)
            {
                PropostasPrestadorVisivel = false;
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao carregar propostas do prestador: {ex.Message}", "OK");
            }
        }


        private async Task AtualizarStatusAsync(int idSolicitacao, int idServico, StatusOrcamentoEnum novoStatus)
        {
            try
            {
                await _solicitacaoService.AtualizarStatusSolicitacaoAsync(idSolicitacao, idServico, novoStatus);
                await App.Current.MainPage.DisplayAlert("Sucesso", "Status atualizado com sucesso", "OK");

                await CarregarSolicitacoesPrestador(IdPrestador);
                await ExibirHistoricoClienteAsync();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao atualizar status: {ex.Message}", "OK");
            }
        }

        public async Task RemoverSolicitacaoAsync(int idServico)
        {
            bool confirmacao = await App.Current.MainPage.DisplayAlert("Confirmar", "Deseja remover esta solicitação?", "Sim", "Não");
            if (!confirmacao) return;

            var sucesso = await _solicitacaoService.RemoverSolicitacaoAsync(idServico);
            if (sucesso)
            {
                await App.Current.MainPage.DisplayAlert("Sucesso", "Solicitação removida com sucesso!", "OK");
                await ExibirHistoricoClienteAsync();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Erro", "Erro ao remover a solicitação.", "OK");
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));



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
            Debug.WriteLine($"✅ Entrou no SelecionarAbaAsync com aba: {aba}");

            HistoricoClienteVisivel = false;
            PropostasClienteVisivel = false;
            SolicitacaoClienteVisivel = false;
            PropostasPrestadorVisivel = false;

            AbaAtual = aba;

            switch (aba)
            {
                case "HistoricoCliente":
                    Debug.WriteLine("🟡 Selecionou HistoricoCliente");
                    HistoricoClienteVisivel = true;
                    await ExibirHistoricoClienteAsync();
                    break;

                case "SolicitacaoCliente":
                    Debug.WriteLine("🟢 Selecionou SolicitacaoCliente");
                    SolicitacaoClienteVisivel = true;
                    await BuscarSolicitacoesDaEmpresaAsync();
                    break;

                case "PropostasRecebidas":
                    Debug.WriteLine("🔵 Selecionou PropostasRecebidas");
                    PropostasClienteVisivel = true;
                    await ExibirPropostasClienteAsync();
                    break;

                case "PropostasPrestador":
                    Debug.WriteLine("🟢 Selecionou PropostasPrestador");
                    PropostasClienteVisivel = false;
                    PropostasPrestadorVisivel = true;
                    await CarregarPropostasPrestadorAsync();
                    break;

            }
        }

        public async Task ExibirPropostasClienteAsync()
        {
            try
            {
                int idCliente = Preferences.Get("id", 0);
                var idEmpresa = IdCliente;
                var servicos = await _perfilService.BuscarPropostasAsync(idEmpresa);

                PropostaCliente.Clear();
                foreach (var servico in servicos)
                    PropostaCliente.Add(servico);

                PropostasClienteVisivel = true;
            }
            catch (Exception ex)
            {
                PropostasClienteVisivel = false;
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao carregar propostas: {ex.Message}", "OK");
            }
        }
        //da

        public async Task BuscarSolicitacoesDaEmpresaAsync()
        {
            try
            {
                int idCliente = Preferences.Get("id", 0);
                var idEmpresa = IdCliente;
                Debug.WriteLine($"🔧 Buscando solicitações para empresa {idEmpresa}");

                var solicitacoes = await _perfilService.BuscarSolicitacoesDaEmpresaAsync(idEmpresa);
                Debug.WriteLine($"🔧 Quantidade de solicitações recebidas: {solicitacoes?.Count}");

                SolicitacaoCliente.Clear();
                foreach (var solicitacao in solicitacoes)
                    SolicitacaoCliente.Add(solicitacao);

                Debug.WriteLine($"🔧 Lista SolicitacaoCliente preenchida com {SolicitacaoCliente.Count} itens");

                SolicitacaoClienteVisivel = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"❌ Erro ao buscar solicitações: {ex.Message}");
                SolicitacaoClienteVisivel = false;
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao carregar solicitações: {ex.Message}", "OK");
            }
        }


        public async Task ExibirHistoricoClienteAsync()
        {
            try
            {
                int idCliente = Preferences.Get("id", 0);
                var idEmpresa = IdCliente;
                var servicos = await _perfilService.BuscarHistoricoAsync(idEmpresa);

                HistoricoCliente.Clear();
                foreach (var servico in servicos)
                    HistoricoCliente.Add(servico);

                HistoricoClienteVisivel = true;
            }
            catch (Exception ex)
            {
                HistoricoClienteVisivel = false;
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao carregar histórico: {ex.Message}", "OK");
            }
        }

        public async Task PagarServicoAsync(int IdServico)
        {
            try
            {
                this.IdServico = IdServico;
                Debug.WriteLine("Botão acionado e chegou na ação");
                var servicoAtualizado = await _servicoService.PagamentoAsync(IdServico);

                // Atualiza o item na lista
                var itemExistente = HistoricoCliente.FirstOrDefault(s => s.IdServico == IdServico);
                if (itemExistente != null)
                {
                    var index = HistoricoCliente.IndexOf(itemExistente);
                    HistoricoCliente[index] = servicoAtualizado;
                }

                await App.Current.MainPage.DisplayAlert("Sucesso", "Pagamento realizado com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}
