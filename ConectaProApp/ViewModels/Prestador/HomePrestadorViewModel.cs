using ConectaProApp.Models;
using ConectaProApp.Services.Prestador;
using ConectaProApp.Services.Servico;
using ConectaProApp.View.Prestador;
using ConectaProApp.ViewModels.Servico;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ServicoModel = ConectaProApp.Models.Servico; // Alias para a classe Servico

namespace ConectaProApp.ViewModels.Prestador
{
    public class HomePrestadorViewModel : BaseViewModel
    {
        private ServicoService sService;

        public ICommand BuscarServicosCommand { get; set; }
        public ICommand ProximoServicoCommand { get; set; }
        public ICommand CandidatarServicoCommand { get; set; }

        public HomePrestadorViewModel()
        {
            CarregarFotoPrestadorAsync();
            sService = new ServicoService();
            BuscarServicosCommand = new Command(async () => await BuscarServicosAsync());
            ProximoServicoCommand = new Command(MostrarProximoServico);
            CandidatarServicoCommand = new Command(async () =>
            {
                var idSolicitacao = ServicoAtual?.IdSolicitacao ?? 0;
                if (idSolicitacao > 0)
                {
                    await Application.Current.MainPage.Navigation.PushAsync(new CandidatarPrestador(idSolicitacao));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Nenhum serviço selecionado.", "OK");
                }
            });
            // busca os serviços ao inicializar a tela
            InitAsync();
        }

        private List<ServicoHomeDTO> servicosUf; // Usando o alias ServicoModel
        private int indice = 0;
       
        private string uf = Preferences.Get("uf", string.Empty);
        public string NomeFantasia => string.IsNullOrEmpty(ServicoAtual?.EmpresaClienteResumoDTO?.NomeFantasia) ? "Empresa sem nome" : ServicoAtual?.EmpresaClienteResumoDTO?.NomeFantasia;
        public string CaminhoFotoEmpresa => string.IsNullOrEmpty(ServicoAtual?.EmpresaClienteResumoDTO?.CaminhoFoto) ? "empresasemfoto.png" : ServicoAtual?.EmpresaClienteResumoDTO?.CaminhoFoto;
        public string DescSolicitacao => string.IsNullOrEmpty(ServicoAtual?.DescSolicitacao) ? "sem descrição" : ServicoAtual?.DescSolicitacao;
        public string Uf => string.IsNullOrEmpty(uf) ? "Sem Uf" : uf;
        public string CaminhoFotoFundo => string.IsNullOrEmpty(ServicoAtual?.EmpresaClienteResumoDTO?.CaminhoFoto) ? "empresasemfoto.png" : ServicoAtual?.EmpresaClienteResumoDTO?.CaminhoFoto;


        private ServicoHomeDTO servicoAtual;
        public ServicoHomeDTO ServicoAtual
        {
            get => servicoAtual;
            set
            {
                servicoAtual = value;

                Debug.WriteLine($"[DEBUG] ServicoAtual atualizado: {servicoAtual?.EmpresaClienteResumoDTO?.NomeFantasia}");
                Debug.WriteLine($"[DEBUG] ServicoAtual atualizado: {servicoAtual?.EmpresaClienteResumoDTO?.CaminhoFoto}");
                Debug.WriteLine($"[DEBUG] ServicoAtual atualizado: {servicoAtual?.DescSolicitacao}");


                OnPropertyChanged();
                OnPropertyChanged(nameof(NomeFantasia));
                OnPropertyChanged(nameof(Uf));
                OnPropertyChanged(nameof(CaminhoFotoEmpresa));
                OnPropertyChanged(nameof(DescSolicitacao));
                OnPropertyChanged(nameof(CaminhoFotoFundo));
            }
        }
        private ImageSource fotoPrestadorUrl;
        public ImageSource FotoPrestadorUrl
        {
            get => fotoPrestadorUrl;
            set
            {
                fotoPrestadorUrl = value;
                OnPropertyChanged();
            }
        }

        private ImageSource fotoEmpresaUrl;
        public ImageSource FotoEmpresaUrl
        {
            get => fotoEmpresaUrl;
            set
            {
                fotoEmpresaUrl = value;
                OnPropertyChanged();
            }
        }

        public async Task InitAsync()
        {
            await CarregarFotoPrestadorAsync();
            await BuscarServicosAsync();
        }

        private async Task BuscarServicosAsync()
        {
            var uf = Preferences.Get("uf", ""); // Ou obtenha de outro lugar
            servicosUf = await sService.BuscarSolictacaoPorUfAsync(uf);

            if (servicosUf.Any())
            {
                Debug.WriteLine("Serviços encontrados: " + servicosUf.Count);
                indice = 0;
                ServicoAtual = servicosUf[indice];
            }
            else
            {
                ServicoAtual = null;
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "Nenhum serviço encontrado para sua região.", "OK");
                });
            }

        }
        private void MostrarProximoServico()
        {
            if (servicosUf == null || servicosUf.Count == 0) return;

            indice = (indice + 1) % servicosUf.Count;
            ServicoAtual = servicosUf[indice];
            Application.Current.MainPage.DisplayAlert("Debug", $"Mostrando: {ServicoAtual.EmpresaClienteResumoDTO?.NomeFantasia}", "OK");
        }

        private async Task CarregarFotoPrestadorAsync()
        {
            var fotoSalva = await SecureStorage.GetAsync("caminhoFoto");

            if (!string.IsNullOrEmpty(fotoSalva))
                FotoPrestadorUrl = ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(fotoSalva)));
            else
                FotoPrestadorUrl = ImageSource.FromFile("prestadorsemfoto.png");
        }
    }
}