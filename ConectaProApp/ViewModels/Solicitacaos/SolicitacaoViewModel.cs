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

namespace ConectaProApp.ViewModels.Solicitacaos
{
    public class SolicitacaoViewModel : INotifyPropertyChanged
    {
        private readonly ApiService _apiService = new ApiService();
        private readonly SolicitacaoService _solicitacaoService;
        private readonly ServicoService _servicoService;
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



        private bool _propostasRecebidasVisivel;
        public bool PropostasRecebidasVisivel
        {
            get => _propostasRecebidasVisivel;
            set
            {
                _propostasRecebidasVisivel = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<ServicoDTO> _propostasRecebidas;
        public ObservableCollection<ServicoDTO> PropostasRecebidas
        {
            get => _propostasRecebidas;
            set
            {
                _propostasRecebidas = value;
                OnPropertyChanged();
            }
        }


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

        private ObservableCollection<SolicitacaoDTO> _propostasPrestador;
        public ObservableCollection<SolicitacaoDTO> PropostasPrestador
        {
            get => _propostasPrestador;
            set
            {
                _propostasPrestador = value;
                OnPropertyChanged();
            }
        }


        public ObservableCollection<SolicitacaoDTO> PropostasCliente { get; set; } = new();
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

            _perfilService = new PerfilEmpresaClienteService();
            HistoricoCliente = new ObservableCollection<ServicoDTO>();
            HistoricoClienteVisivel = false;
            _servicoService = new ServicoService();
            _perfilPrestadorService = new PerfilPrestadorService();

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
            RemoverSolicitacaoCommand = new Command<int>(async (idSolicitacao) => await RemoverSolicitacaoAsync(idSolicitacao));
            PagarServicoCommand = new Command<int>(async (IdServico) => await PagarServicoAsync(IdServico));
            SelecionarAbaCommand = new Command<string>(async (aba) => await SelecionarAbaAsync(aba));
            SelecionarFotoCommand = FotoVMAvatar.SelecionarFotoCommand;
            ConfirmarFinalizacaoServicoCommand = new Command<int>(async (idServico) => await ConfirmarFinalizacaoServicoAsync(idServico));
        }

        public SolicitacaoViewModel() : this(TipoUsuario.Cliente, Preferences.Get("id", 0)) { }



        /*
        private async Task EnviarPropostaAsync(int idSolicitacao)
        {
            try
            {
                var proposta = new ServicoDTO
                {
                    IdEmpresaCliente = IdCliente,
                    IdPrestador = IdPrestador,
                    IdSolicitacao = idSolicitacao,
                    Descricao = Descricao,
                    ValorContratacao = ValorContratacao,
                };

                await _solicitacaoService.EnviarPropostaAsync(idSolicitacao, proposta);
                await Application.Current.MainPage.DisplayAlert("Sucesso", "Proposta enviada com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", $"Falha ao enviar proposta: {ex.Message}", "OK");
            }
        
        */

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

        public async Task RemoverSolicitacaoAsync(int idSolicitacao)
        {
            bool confirmacao = await App.Current.MainPage.DisplayAlert("Confirmar", "Deseja remover esta solicitação?", "Sim", "Não");
            if (!confirmacao) return;

            var sucesso = await _solicitacaoService.RemoverSolicitacaoAsync(idSolicitacao);
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
            try
            {
                Debug.WriteLine($"🔵 Aba selecionada: {aba}");

                HistoricoClienteVisivel = false;
                PropostasClienteVisivel = false;
                SolicitacoesClienteVisivel = false;
                ServicosPrestadorVisivel = false;

                AbaAtual = aba;

                switch (aba)
                {
                    case "HistoricoCliente":
                        HistoricoClienteVisivel = true;
                        Debug.WriteLine("🔵 Carregando histórico do cliente...");
                        await ExibirHistoricoClienteAsync();
                        break;

                    case "PropostasRecebidas":
                        PropostasRecebidasVisivel = true;
                        await ExibirPropostasRecebidasAsync();
                        break;

                    case "PropostasPrestador":
                        PropostasRecebidasVisivel = true;
                        await ExibirPropostasPrestadorAsync();
                        break;



                    case "ServicosPrestador":
                        ServicosPrestadorVisivel = true;
                        Debug.WriteLine("🔵 Carregando serviços do prestador...");
                        await CarregarSolicitacoesPrestador(IdPrestador);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Erro ao selecionar aba: " + ex.Message);
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao carregar a aba: {ex.Message}", "OK");
            }
        }


        // está funcionando. ->
        public async Task ExibirHistoricoClienteAsync()
        {
            try
            {
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


        //está funcionando. ->
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


        public async Task ExibirPropostasRecebidasAsync()
        {
            try
            {
                var propostas = await _perfilService.BuscarPropostasAsync(IdCliente);
                PropostasRecebidas = new ObservableCollection<ServicoDTO>(propostas);


                PropostasClienteVisivel = true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao carregar propostas: {ex.Message}", "OK");
            }
        }

        public async Task ConfirmarFinalizacaoServicoAsync(int IdServico)
        {
            try
            {
              
                this.IdServico = IdServico;
                Debug.WriteLine("Botão acionado e chegou na ação de confirmação de finalização");

                var servicoAtualizado = await _servicoService.ConfirmarFinalizacaoServicoAsync(IdServico);

                // Atualiza o item na lista
                var itemExistente = HistoricoCliente.FirstOrDefault(s => s.IdServico == IdServico);
                if (itemExistente != null)
                {
                    var index = HistoricoCliente.IndexOf(itemExistente);
                    HistoricoCliente[index] = servicoAtualizado;
                }

                // Atualiza a DataFinalizacao na própria ViewModel
                DataFinalizacao = servicoAtualizado.DataFinalizacao;

                await App.Current.MainPage.DisplayAlert("Sucesso", "Finalização confirmada com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Erro", ex.Message, "OK");
            }
        }

        public async Task ExibirPropostasPrestadorAsync()
        {
            try
            {
                Debug.WriteLine("🔵 Iniciando carregamento de propostas do prestador...");

                var propostas = await _perfilPrestadorService.BuscarPropostasPrestadorAsync(IdPrestador);

                PropostasRecebidas.Clear();

                foreach (var proposta in propostas)
                {
                    PropostasRecebidas.Add(proposta);
                }

                PropostasRecebidasVisivel = true;
                Debug.WriteLine($"🔵 Total de propostas carregadas: {propostas.Count}");
            }
            catch (Exception ex)
            {
                PropostasRecebidasVisivel = false;
                Debug.WriteLine("Erro ao carregar propostas do prestador: " + ex.Message);
                await App.Current.MainPage.DisplayAlert("Erro", $"Erro ao carregar propostas: {ex.Message}", "OK");
            }
        }



    }
}
